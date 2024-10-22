using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Revenues.Register;
public interface IRegisterRevenueUseCase
{
    Task<ResponseRegisteredRevenueJson> Execute(RequestRevenueJson request);
}
