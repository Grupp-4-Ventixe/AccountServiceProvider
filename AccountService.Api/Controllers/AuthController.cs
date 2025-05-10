using AccountService.Business.DTOs;
using AccountService.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

       

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto formData)
        {
            if (!ModelState.IsValid)       
                return BadRequest(ModelState);

            var result = await _authService.SignUpAsync(formData);
            return result.Succeeded
                ? Ok(result) 
                : Problem(result.Message);

        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.SignInAsync(formData);
            if (result.Succeeded)
                return Ok(result);

            return Unauthorized("Invalid credentials.");

        }
    }
}
