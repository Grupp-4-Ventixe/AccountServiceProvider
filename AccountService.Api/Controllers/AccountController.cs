using AccountService.Business.DTOs;
using AccountService.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
   
    private readonly IAccountService _accountService = accountService;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateUserDto formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _accountService.CreateAccountAsync(formData);

        if (!result.Succeeded)
        {
            return BadRequest(new { Errors = result.Errors });
        }

        return Ok(new { Message = "Account created successfully with role." });
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAccountById(string userId)
    {
        var account = await _accountService.GetAccountByIdAsync(userId);

        if (account == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        return Ok(account);
    }
}
