using Infrastructure.Dapper;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;

namespace confitec_ssss.Configuration;

public static class Bootstrapper
{
    #region Methods
    public static IServiceCollection AddComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen()
                .Configure<AppSettings>(configuration.GetSection("AppSettings"))
                .AddServices(configuration);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Infrastructure.EntityFramework.UserContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Confitec")));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IDatabaseConnectionFactory>(f => new SqlConnectionFactory(configuration.GetConnectionString("Confitec")));
        return services;
    }
    #endregion Methods
}