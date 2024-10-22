using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Security.Cryptography;
using BarberBossManagement.Domain.Security.Tokens;
using BarberBossManagement.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{

    public RevenueIdentityManager RevenueManager { get; private set; } = default!;
    public UserIdentityManager User_Team_Member { get; private set; } = default!;
    public UserIdentityManager User_Admin { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<BarberBossManagementDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<BarberBossManagementDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncripter, accessTokenGenerator);

            });
    }


    //public long GetRevenueId() => _revenue.Id;
    private void StartDatabase(
        BarberBossManagementDbContext dbContext,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {


        var user = AddUserTeamMember(dbContext, passwordEncripter, accessTokenGenerator);
        AddRevenues(dbContext, user);

        dbContext.SaveChanges();
    }

    private User AddUserTeamMember(
        BarberBossManagementDbContext dbContext,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        var password = user.Password;

        user.Password = passwordEncripter.Encrypt(user.Password);

        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);

        User_Team_Member = new UserIdentityManager(user, password, token);

        return user;
    }

    //private void AddRevenues(BarberBossManagementDbContext dbContext, User user)
    //{
    //    var revenue = RevenueBuilder.Build(user);
    //
    //    dbContext.Revenues.Add(revenue);
    //}

    private void AddRevenues(BarberBossManagementDbContext dbContext, User user)
    {
        var revenue = RevenueBuilder.Build(user);

        dbContext.Revenues.Add(revenue);

        RevenueManager = new RevenueIdentityManager(revenue);
    }

}
