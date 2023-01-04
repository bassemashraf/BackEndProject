using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Dtos.User;
using BackEnd.models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{  
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("regestir")]
        public async Task<ActionResult<ServiceResponse<int>>> Register (AddUserDto request)
        {
            var response = await _authRepo.Register(new User {UserName = request.UserName}, request.Password
            );
            if(!response.Succes)
            {
                return BadRequest(response);
            }
            return Ok(response);
        } 



         [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login (UserLoginDTO request)
        {
            var response = await _authRepo.Login(request.UserName,request.Password);
            
            if(!response.Succes)
            {
                return BadRequest(response);
            }
            return Ok(response);
        } 

        
    }
}