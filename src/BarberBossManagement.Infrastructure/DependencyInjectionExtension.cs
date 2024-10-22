using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.Barbers;
using BarberBossManagement.Domain.Repositories.Revenues;
using BarberBossManagement.Domain.Repositories.User;
using BarberBossManagement.Domain.Security.Cryptography;
using BarberBossManagement.Domain.Security.Tokens;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Infrastructure.DataAccess;
using BarberBossManagement.Infrastructure.DataAccess.Repositories;
using BarberBossManagement.Infrastructure.Extensions;
using BarberBossManagement.Infrastructure.Services.LoggedUser;
using BarberBossManagement.Infrastructure.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBossManagement.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddToken(services, configuration);
        AddRepositories(services);


        if(configuration.IsTestEnvironment() == false)
        {
            AddDbContext(services, configuration);
        }

    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    // Essa função AddToken é uma extenção de configuração de serviços no ASP.NET Core, usada para registrar
    // a lógica de geração de tokens JWT no conteiner de injeção de dependencias da aplicação. Ela configura
    // a geração de tokens JWT para autenticação, utilizando a classe JwtTokenGenerator, que implementa a interface
    // IAccessTokenGenerator
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRevenuesWriteOnlyRepository, RevenuesRepository>();
        services.AddScoped<IRevenuesReadOnlyRepository, RevenuesRepository>();
        services.AddScoped<IRevenueUpdateOnlyRepository, RevenuesRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IBarbersShopsWriteOnlyRepository, BarbersRepository>();
        services.AddScoped<IBarbersShopsReadOnlyRepository, BarbersRepository>();
        services.AddScoped<IBarbersShopsUpdateOnlyRepository, BarbersRepository>();

    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection"); ;
        var version = new Version(8, 0, 39);
        var serverVersion = new MySqlServerVersion(version);

      

        services.AddDbContext<BarberBossManagementDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
