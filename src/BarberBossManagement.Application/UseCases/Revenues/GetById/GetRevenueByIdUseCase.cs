using AutoMapper;
using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Domain.Repositories.Revenues;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Revenues.GetById;
public class GetRevenueByIdUseCase : IGetRevenueByIdUseCase
{
    private readonly IRevenuesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetRevenueByIdUseCase(
        IRevenuesReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRevenueJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetById(loggedUser, id);

        if(result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.REVENUE_NOT_FOUND);
        }

        return _mapper.Map<ResponseRevenueJson>(result);
    }
}
