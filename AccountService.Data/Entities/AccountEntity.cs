using Microsoft.AspNetCore.Identity;

namespace AccountService.Data.Entities;

public class AccountEntity : IdentityUser
{
    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; }

}
