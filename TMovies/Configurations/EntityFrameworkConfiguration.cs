using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TMovies.Data;

public static class EntityFrameworkConfiguration
{
    public static void ConfigureEntityFramework(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
