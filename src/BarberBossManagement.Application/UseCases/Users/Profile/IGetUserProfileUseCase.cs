using BarberBossManagement.Communication.Responses;

namespace BarberBossManagement.Application.UseCases.Users.Profile;
public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
