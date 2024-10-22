using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Repositories.Revenues;
using Microsoft.EntityFrameworkCore;

namespace BarberBossManagement.Infrastructure.DataAccess.Repositories;
internal class RevenuesRepository : IRevenuesReadOnlyRepository, IRevenuesWriteOnlyRepository, IRevenueUpdateOnlyRepository
// A classe é definida como internal, o que significa que ela só pode ser acessada dentro do mesmo projeto
// Isso é útil para encapsular a lógica de acesso a dados e manter a implementação oculta de outras partes
// da aplicação
{
    private readonly BarberBossManagementDbContext _dbContext; // _dbContext é uma instância do BarberBossManagementDbContext
    public RevenuesRepository(BarberBossManagementDbContext dbContext)
    // O construtor recebe um BarberBossManagementDbContext como parãmetro e o armazena na variável privada
    // _dbContext. Isso permite que os métodos da classe realizem operações no banco de dados
    {
        _dbContext = dbContext;
    }
    public async Task Add(Revenue revenue)
    {
        // Esse método é responsável por adicionar uma nova receita ao banco de dados
        // O método usa AddAsync, que é uma operação assíncrona. Isso evita bloquear a thread enquanto a receita
        // está sendo adicionada
        await _dbContext.Revenues.AddAsync(revenue);
    }

    public async Task Delete(long id)
    {
        // Esse método busca um faturamento pelo ID e a remove do banco de dados
        // O método FindAsync(id) procura o faturamento correspondente ao ID fornecido
        var result = await _dbContext.Revenues.FindAsync(id);

        _dbContext.Revenues.Remove(result!);
    }

    public async Task<List<Revenue>> GetAll(User user)
    {
        // O método GetAll precisa do User como parâmetro para filtrar as receitas do banco de dados de acordo
        // com o usuário que está solicitando os dados. Isso ocorre porque a aplicação deve retornar apenas as
        // receitas associadas ao usuario que está logado. No caso de múltiplos usuários usando o sistema,
        // essa filtragem é essencial para garantir que um usuário não acesse os dados do outro
        return await _dbContext.Revenues.AsNoTracking().Where(revenue => revenue.UserId == user.Id).ToListAsync();
    }

    async Task<Revenue?> IRevenuesReadOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.Revenues
            .AsNoTracking()
            .Include(revenue => revenue.BarberShop)
            .FirstOrDefaultAsync(revenue => revenue.Id == id && revenue.UserId == user.Id);
    }

    async Task<Revenue?> IRevenueUpdateOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.Revenues.FirstOrDefaultAsync(revenue => revenue.Id == id && revenue.UserId == user.Id);
    }

    public void Update(Revenue revenue)
    {
        _dbContext.Revenues.Update(revenue);
    }

    //public async Task<List<Revenue>> FilterByWeek(DateTime startDate)
    //{
    //    var endDate = startDate.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
    //    
    //
    //    return await _dbContext
    //        .Revenues
    //        .AsNoTracking()
    //        .Where(revenue => revenue.Date >= startDate && revenue.Date <= endDate)
    //        .OrderBy(revenue => revenue.Date)
    //        .ToListAsync();
    //}

    public async Task<List<Revenue>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);

        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Revenues
            .AsNoTracking()
            .Where(revenue => revenue.Date >= startDate && revenue.Date <= endDate)
            .OrderBy(revenue => revenue.Date)
            .ToListAsync();
    }
}
