using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
