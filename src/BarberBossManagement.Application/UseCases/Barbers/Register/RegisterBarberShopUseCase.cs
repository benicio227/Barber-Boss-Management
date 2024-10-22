using AutoMapper;
using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.Barbers;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Barbers.Register;
public class RegisterBarberShopUseCase : IRegisterBarberShopUseCase
{
    public readonly IBarbersShopsWriteOnlyRepository _repository;
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterBarberShopUseCase(
        IBarbersShopsWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseBarberShopJson> Execute(RequestBarberShopJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var barberShop = _mapper.Map<BarberShop>(request);
        barberShop.UserId = loggedUser.Id;

        await _repository.Add(barberShop);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseBarberShopJson>(barberShop);
    }

    private void Validate(RequestBarberShopJson request)
    {
        var validator = new BarberValidator();

        var result = validator.Validate(request);

        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

        if (result.IsValid is false)
        {
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
