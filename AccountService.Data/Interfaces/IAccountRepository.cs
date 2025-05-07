using AccountService.Data.Entities;

namespace AccountService.Data.Interfaces;

public interface IAccountRepository : IBaseRepository<AccountEntity>
{
    Task<AccountEntity?> GetUserByIdAsync(string userId);
}