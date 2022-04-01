using System.Threading.Tasks;
using CarRentalRestApi.Data.Repository;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalRestApi.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public UserController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register([FromBody]UserRegisterDto request)
        {
            var response = await _authRepository.Register(
                new User
                {
                    Email = request.Email, 
                    FirstName = request.FirstName, 
                    LastName = request.LastName
                }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login([FromBody]UserLoginDto request)
        {
            var response = await _authRepository.Login(
                request.Email, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}