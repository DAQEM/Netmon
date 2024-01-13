using System.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Netmon.Identity;

public static class IdentityExtensions
{
    public static IdentityBuilder AddNetmonIdentity(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();
        
        string connectionString = configurationManager["MySQL:Identity:ConnectionString"] ?? throw new InvalidOperationException("MySQL connection string is not configured.");

        services.AddDbContext<Database>(options =>
        {
            options.UseMySQL(connectionString);
        });
        
        return services.AddIdentityCore<User>().AddEntityFrameworkStores<Database>();
    }
    
    public static void AddNetmonIdentityWithApiEndpoints(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddNetmonIdentity(configurationManager).AddApiEndpoints();
    }
    
    public static void UserNetmonIdentityWithApiEndpoints(this WebApplication app)
    {
        app.MapIdentityApi<User>();
    }
}