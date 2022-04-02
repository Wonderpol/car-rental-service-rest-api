using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalRestApi.Data;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models;
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

        public AuthService(DataContext dataContext, IConfiguration configuration, IMapper mapper)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _mapper = mapper;
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

        public async Task<ServiceResponse<string>> Login(string email , string password)
        {
            var response = new ServiceResponse<string>();
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
                response.Data = GenerateToken(user);
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

        private string GenerateToken(User user)
        {
            
            //Allows to read user id and user email from token without password
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
                );

            var loginCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = loginCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
        
    }
}