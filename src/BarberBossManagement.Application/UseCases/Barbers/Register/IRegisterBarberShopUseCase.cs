using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Barbers.Register;
public interface IRegisterBarberShopUseCase
{
    Task<ResponseBarberShopJson> Execute(RequestBarberShopJson request);
}
