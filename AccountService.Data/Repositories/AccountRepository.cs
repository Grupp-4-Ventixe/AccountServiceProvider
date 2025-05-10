using AccountService.Data.Contexts;
using AccountService.Data.Entities;
using AccountService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace AccountService.Data.Repositories;

public class AccountRepository : BaseRepository<AccountEntity>, IAccountRepository
{
    public AccountRepository(AccountDbContext context) : base(context)
    {
    }

    public async Task<AccountEntity?> GetUserByIdAsync(string userId)
    {
        return await _context.AccountEntities
            .Where(account => account.Id == userId)
            .FirstOrDefaultAsync();
    }
}
