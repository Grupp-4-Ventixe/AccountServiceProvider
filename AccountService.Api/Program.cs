using AccountService.Business.Interfaces;
using AccountService.Business.Services;
using AccountService.Data.Contexts;
using AccountService.Data.Interfaces;
using AccountService.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AccountDbContext>();


builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IAccountService, AccountService.Business.Services.AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "Member" };

    foreach(var roleName in roleNames)
    {
       var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var user = new IdentityUser { UserName = "admin@ventixe.com", Email = "admin@ventixe.com" };

    var userExists = await userManager.Users.AnyAsync(x => x.Email == user.Email);
    if (!userExists)
    {
        var result = await userManager.CreateAsync(user, "BytMig123!");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, "Admin");

    }
}


app.Run();



