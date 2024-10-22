using AutoMapper;
using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.Barbers;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Barbers.Update;
public class UpdateBarberUseCase : IUpdateBarberUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBarbersShopsUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    public UpdateBarberUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IBarbersShopsUpdateOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(long id, RequestBarberShopJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var barber = await _repository.GetById(loggedUser, id);

        if (barber is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BARBER_NOT_FOUND);
        }

        _mapper.Map(request, barber);

        _repository.Update(barber);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestBarberShopJson request)
    {
        var validate = new BarberValidator();

        var result = validate.Validate(request);

        if(result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }


    }
}
