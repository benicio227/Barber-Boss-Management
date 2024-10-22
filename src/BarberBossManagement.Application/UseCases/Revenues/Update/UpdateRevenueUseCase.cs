using AutoMapper;
using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.Revenues;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Revenues.Update;
public class UpdateRevenueUseCase : IUpdateRevenueUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRevenueUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    public UpdateRevenueUseCase(IMapper mapper,
        IUnitOfWork unitOfWork,
        IRevenueUpdateOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
        _loggedUser = loggedUser;
    }
    public async Task Execute(long id, RequestRevenueJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var revenue = await _repository.GetById(loggedUser, id);

        if(revenue is null)
        {
            throw new NotFoundException(ResourceErrorMessages.REVENUE_NOT_FOUND);
        }

        _mapper.Map(request, revenue);

        _repository.Update(revenue);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestRevenueJson request)
    {
        var validate = new RevenueValidator();

        var result = validate.Validate(request);

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
