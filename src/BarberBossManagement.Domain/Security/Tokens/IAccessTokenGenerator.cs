using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
