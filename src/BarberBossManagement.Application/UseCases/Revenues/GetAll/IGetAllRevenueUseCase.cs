using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Revenues.GetAll;
public interface IGetAllRevenueUseCase
{
    Task<ResponseRevenuesJson> Execute();
}
