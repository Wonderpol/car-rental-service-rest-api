using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Utils.AuthUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalRestApi.Services.AuthService
{
    public class AuthService: IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IJwtTokenUtils _jwtTokenUtils;

        public AuthService(DataContext dataContext, IConfiguration configuration, IMapper mapper, IJwtTokenUtils jwtTokenUtils)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _mapper = mapper;
            _jwtTokenUtils = jwtTokenUtils;
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
            }

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

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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