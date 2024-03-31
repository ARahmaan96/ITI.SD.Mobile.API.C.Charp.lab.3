using Lab3.DTOs;
using Lab3.Services.AccountServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace Lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                object? result = await _accountServices.Register(userDTO)!;

                if (result != null && result is bool)
                    return Ok(new { msg = "User Added Succefully" });

                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                string? token = await _accountServices.Login(userDTO)!;

                if (token == null)
                    return Unauthorized();

                return Ok(new
                {
                    msg = "Valid User!",
                    token = token,
                });
            }
            return BadRequest(ModelState);
        }
    }
}
