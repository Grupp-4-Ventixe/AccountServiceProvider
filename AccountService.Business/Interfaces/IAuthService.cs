using AccountService.Business.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Business.Interfaces
{
    public interface IAuthService
    {
        Task LogoutAsync();
        Task<string> SignInAsync(SignInDto formData);
        Task<SignUpResult> SignUpAsync(SignUpDto formData);
        Task<bool> UserExistsAsync(string email);
    }
}