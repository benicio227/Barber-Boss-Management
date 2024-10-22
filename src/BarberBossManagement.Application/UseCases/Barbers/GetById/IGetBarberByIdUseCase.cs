using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Barbers.GetById;
public interface IGetBarberByIdUseCase
{
    Task<ResponseBarberShopJson> Execute(long id);
}
