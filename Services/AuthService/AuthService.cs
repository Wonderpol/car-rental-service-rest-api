using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Repository;
using CarRentalRestApi.Utils.AuthUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRentalRestApi.Services.AuthService
{
    public class AuthService: IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IJwtTokenUtils _jwtTokenUtils;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(DataContext dataContext, IConfiguration configuration, IMapper mapper, IJwtTokenUtils jwtTokenUtils, IRefreshTokenRepository refreshTokenRepository)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _jwtTokenUtils = jwtTokenUtils;
            _refreshTokenRepository = refreshTokenRepository;
        }
        
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            response.Data = user.Id;

            return response;
        }

        public async Task<LoginResponse> Login(string email , string password)
        {
            var response = new LoginResponse();
            var user = await _dataContext.Users.FirstOrDefaultAsync(usr => usr.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            } else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong email or password";
            }
            else
            {
                var access = _jwtTokenUtils.GenerateAccessToken(user);
                var refresh = _jwtTokenUtils.GenerateRefreshToken();
                response.Access = access;
                response.Refresh = refresh;
                response.Data = _mapper.Map<UserGetDto>(user);
                response.Message = "Successfully logged in";
                response.isAdmin = user.Role == Roles.Admin;
                
                //Try to add refresh token to repository
                var refreshToken = new RefreshToken
                {
                    UserId = user.Id,
                    Token = refresh
                };
                await _refreshTokenRepository.AddToken(refreshToken);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> Logout(int id)
        {
            var removeToken = await _refreshTokenRepository.RemoveToken(id);
            var response = new ServiceResponse<bool>();
            
            if (!removeToken)
            {
                response.Data = false;
                response.Message = "Unexpected error";
                response.Success = false;
                return response;
            }

            response.Data = removeToken;
            response.Message = "Logged out";
            response.Success = true;

            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _dataContext.Users.AnyAsync(user => user.Email.Equals(email));
        }

        public async Task<ServiceResponse<UserGetDto>> GetMe(int id)
        {
            var response = new ServiceResponse<UserGetDto>();
            var user =  await _dataContext.Users.FirstOrDefaultAsync(usr => usr.Id.Equals(id));
            if (user == null)
            {
                response.Success = false;
                response.Message = "Something went wrong";
            }
            else
            {
                response.Success = true;
                response.Data = _mapper.Map<UserGetDto>(user);
            }

            return response;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(usr => usr.Id.Equals(id));
        }

        public async Task<LoginResponse> RefreshToken(string token)
        {
            var response = new LoginResponse();

            var isValidRefreshToken = _jwtTokenUtils.ValidateRefreshToken(token);
            if (!isValidRefreshToken)
            {
                response.Success = false;
                response.Message = "Token expired or is invalid";
                return response;
            }

            var refreshToken = await _refreshTokenRepository.GetTokenByToken(token);

            if (refreshToken == null)
            {
                response.Success = false;
                response.Message = "Token expired or is invalid";
                return response;
            }

            var user = await GetUserById(refreshToken.UserId);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Some error occured";
                return response;
            }
            
            var access = _jwtTokenUtils.GenerateAccessToken(user);
            var refresh = _jwtTokenUtils.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refresh
            };
            await _refreshTokenRepository.AddToken(newRefreshToken);

           response.Access = access;
           response.Refresh = refresh;
           response.Success = true;

           return response;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (var i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}