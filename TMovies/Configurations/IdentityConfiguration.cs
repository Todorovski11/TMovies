using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TMovies.Data;

public static class IdentityConfiguration
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}
