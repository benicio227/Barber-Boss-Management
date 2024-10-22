using BarberBossManagement.Domain.Repositories;

namespace BarberBossManagement.Infrastructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
// A classe é declarada como internal, o que significa que ela só pode ser acessada dentro do mesmo assembly
// Isso é útil para restringir o acesso a classes que não precisam ser expostas publicamente
{
    private readonly BarberBossManagementDbContext _dbContext; 
    public UnitOfWork(BarberBossManagementDbContext dbContext) // O construtor recebe um objeto do tipo BarberBossManagementDbContext
    {
        _dbContext = dbContext;
    }
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
        // O método SaveChangesAsync é responsável por persistir todas as alterações feitas no contexto de dados
        //(como inserções, atualizações e exclusões) ao banco de dados.
    }
}
