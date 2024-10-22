using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Repositories.Barbers;
public interface IBarbersShopsUpdateOnlyRepository
{
    Task<BarberShop?> GetById(Domain.Entities.User user, long id);
    void Update(BarberShop barber);
}
