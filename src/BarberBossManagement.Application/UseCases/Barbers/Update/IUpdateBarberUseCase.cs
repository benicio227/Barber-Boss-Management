using BarberBossManagement.Communication.Requests;

namespace BarberBossManagement.Application.UseCases.Barbers.Update;
public interface IUpdateBarberUseCase
{
    Task Execute(long id, RequestBarberShopJson request);
}
