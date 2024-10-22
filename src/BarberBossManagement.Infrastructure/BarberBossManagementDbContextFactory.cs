using BarberBossManagement.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BarberBossManagement.Infrastructure
{
    public class BarberBossManagementDbContextFactory : IDesignTimeDbContextFactory<BarberBossManagementDbContext>
    {
        public BarberBossManagementDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BarberBossManagementDbContext>();
            var connectionString = "Server=localhost;Database=barberbossmanagement;Uid=root;Pwd=@Password123;"; // Adicione a string de conexão adequada

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 39)));

            return new BarberBossManagementDbContext(optionsBuilder.Options);
        }
    }
}