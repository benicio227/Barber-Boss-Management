using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Revenues.GetById;
public interface IGetRevenueByIdUseCase
{
    Task<ResponseRevenueJson> Execute(long id);
}
