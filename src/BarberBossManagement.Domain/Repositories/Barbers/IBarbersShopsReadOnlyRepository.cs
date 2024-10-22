using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Repositories.Barbers;
public interface IBarbersShopsReadOnlyRepository
{
    Task<List<BarberShop>> GetAll(Entities.User user);

    Task<BarberShop?> GetById(Entities.User user, long id);
}
