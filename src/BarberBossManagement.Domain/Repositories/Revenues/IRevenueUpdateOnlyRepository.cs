using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Repositories.Revenues;
public interface IRevenueUpdateOnlyRepository
{
    Task<Revenue?> GetById(Domain.Entities.User user, long id);
    void Update(Revenue revenue);
}
