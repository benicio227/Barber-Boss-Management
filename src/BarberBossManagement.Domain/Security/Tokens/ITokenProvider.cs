namespace BarberBossManagement.Domain.Security.Tokens;
public interface ITokenProvider
{
    string TokenOnRequest();
}
