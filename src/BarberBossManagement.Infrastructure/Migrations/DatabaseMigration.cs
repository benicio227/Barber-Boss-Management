using BarberBossManagement.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBossManagement.Infrastructure.Migrations;
public static class DataBaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<BarberBossManagementDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
