using AccountService.Business.DTOs;
using AccountService.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Business.Services;


public class AuthService : IAuthService
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignUpResult> SignUpAsync(SignUpDto formData)
    {
        var user = new IdentityUser
        {
            UserName = formData.Email,
            Email = formData.Email
        };

        var result = await _userManager.CreateAsync(user, formData.Password);

        return new SignUpResult
        {
            Succeeded = false,
            Errors = new List<string>()

        };
    }
    public async Task<SignInResult> SignInAsync(SignInDto formData)
    {
        return await _signInManager.PasswordSignInAsync(
            formData.Email,
            formData.Password,
            formData.RememberMe,
            lockoutOnFailure: false
        );
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }


}
