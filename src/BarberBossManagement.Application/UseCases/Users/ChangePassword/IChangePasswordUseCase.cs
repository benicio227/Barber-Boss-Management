using BarberBossManagement.Communication.Requests;

namespace BarberBossManagement.Application.UseCases.Users.ChangePassword;
public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
