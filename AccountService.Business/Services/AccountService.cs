using AccountService.Business.DTOs;
using AccountService.Business.Interfaces;
using AccountService.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Business.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAccountRepository _accountRepository;

    public AccountService(UserManager<IdentityUser> userManager, IAccountRepository accountRepository)
    {
        _userManager = userManager;
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto> GetAccountByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return null;

        var account = await _accountRepository.GetUserByIdAsync(userId);

        return new AccountDto
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName
        };
    }

}
