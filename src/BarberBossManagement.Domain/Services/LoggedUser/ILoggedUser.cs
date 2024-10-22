using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
