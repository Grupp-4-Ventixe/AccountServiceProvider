using AccountService.Business.DTOs;

namespace AccountService.Business.Interfaces
{
    public interface IAccountService
    {
        Task<CreateUserResult> CreateAccountAsync(CreateUserDto formData);
        Task<AccountDto> GetAccountByIdAsync(string userId);
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
     
    }
}