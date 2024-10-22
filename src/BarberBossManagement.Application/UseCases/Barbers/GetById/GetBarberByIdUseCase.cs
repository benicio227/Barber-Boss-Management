using AutoMapper;
using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Domain.Repositories.Barbers;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Barbers.GetById;
public class GetBarberByIdUseCase : IGetBarberByIdUseCase
{
    private readonly IBarbersShopsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetBarberByIdUseCase(
        IBarbersShopsReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseBarberShopJson> Execute(long id)
    {
      
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetById(loggedUser, id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BARBER_NOT_FOUND);
        }

        return _mapper.Map<ResponseBarberShopJson>(result);
    }
}
