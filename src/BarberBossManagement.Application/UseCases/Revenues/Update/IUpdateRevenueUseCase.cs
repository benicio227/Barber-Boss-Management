using BarberBossManagement.Communication.Requests;

namespace BarberBossManagement.Application.UseCases.Revenues.Update;
public interface IUpdateRevenueUseCase
{
    Task Execute(long id, RequestRevenueJson request);
}
