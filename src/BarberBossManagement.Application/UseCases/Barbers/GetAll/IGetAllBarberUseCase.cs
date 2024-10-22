using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Barbers.GetAll;
public interface IGetAllBarberUseCase
{
    Task<ResponseBarbersShopJson> Execute();
}
