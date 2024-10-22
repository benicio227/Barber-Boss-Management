using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Repositories.Barbers;
public interface IBarbersShopsWriteOnlyRepository
{
    Task Add(BarberShop barber);

    Task Delete(long id);
}
