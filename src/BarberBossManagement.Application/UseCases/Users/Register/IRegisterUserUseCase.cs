using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
