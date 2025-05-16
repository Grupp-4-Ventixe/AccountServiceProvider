using AccountService.Business.DTOs;

namespace AccountService.Business.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountByIdAsync(string userId);
        Task<CreateUserResult> CreateAccountAsync(CreateUserDto formData);
    }
}