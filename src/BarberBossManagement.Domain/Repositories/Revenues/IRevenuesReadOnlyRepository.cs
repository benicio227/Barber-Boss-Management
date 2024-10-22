using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Repositories.Revenues;
public interface IRevenuesReadOnlyRepository
{
    Task<List<Revenue>> GetAll(Entities.User user); // O método aceita um parâmetro do tipo Entities.User,
    // que representa o usuário cujas receitas estão sendo solicitadas. Isso permite que o método filtre as
    // receitas retornadas com base no usuário
    Task<Revenue?> GetById(Entities.User user, long id);

    //Task<List<Revenue>> FilterByWeek(DateTime startDate);
    Task<List<Revenue>> FilterByMonth(DateOnly date);
}
