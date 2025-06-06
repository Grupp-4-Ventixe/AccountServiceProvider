﻿using AccountService.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Data.Contexts;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<AccountEntity> AccountEntities { get; set; }
}
