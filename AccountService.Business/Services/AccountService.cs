using AccountService.Business.DTOs;
using AccountService.Business.Interfaces;
using AccountService.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Business.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAccountRepository _accountRepository;


    public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IAccountRepository accountRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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

    public async Task<CreateUserResult> CreateAccountAsync(CreateUserDto formData)
    {
        var user = new IdentityUser
        {
            UserName = formData.Email,
            Email = formData.Email
        };
        var createUserResult = await _userManager.CreateAsync(user, formData.Password);
        if (!createUserResult.Succeeded)
        {
            return new CreateUserResult
            {
                Succeeded = false,
                Errors = createUserResult.Errors.Select(e => e.Description).ToList()
            };
        }

        if(!await _roleManager.RoleExistsAsync(formData.Role))
        {
            var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(formData.Role));
            if(!createRoleResult.Succeeded)
            {
                return new CreateUserResult
                {
                    Succeeded = false,
                    Errors = createRoleResult.Errors.Select(e => e.Description).ToList()
                };
            }
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, formData.Role);
        if(!addToRoleResult.Succeeded)
        {
            return new CreateUserResult
            {
                Succeeded = false,
                Errors = addToRoleResult.Errors.Select(e => e.Description).ToList()
            };
        }

        return new CreateUserResult
        {
            Succeeded = true,
            Errors = new List<string>()
        };

    }



}
