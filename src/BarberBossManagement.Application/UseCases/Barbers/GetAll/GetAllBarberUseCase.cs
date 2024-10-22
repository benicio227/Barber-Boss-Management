using AutoMapper;
using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Domain.Repositories.Barbers;
using BarberBossManagement.Domain.Services.LoggedUser;

namespace BarberBossManagement.Application.UseCases.Barbers.GetAll;
public class GetAllBarberUseCase : IGetAllBarberUseCase
{
    private readonly IBarbersShopsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetAllBarberUseCase(
        IBarbersShopsReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseBarbersShopJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser);

        return new ResponseBarbersShopJson
        {
            Barbers = _mapper.Map<List<ResponseShortBarberJson>>(result),
        };
    }
}
