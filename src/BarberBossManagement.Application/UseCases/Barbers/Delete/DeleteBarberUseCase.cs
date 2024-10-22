
using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.Barbers;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Barbers.Delete;
public class DeleteBarberUseCase : IDeleteBarberUseCase
{
    private readonly IBarbersShopsReadOnlyRepository _revenueReadOnly;
    private readonly IBarbersShopsWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    public DeleteBarberUseCase(
        IBarbersShopsReadOnlyRepository revenueReadOnly,
        IBarbersShopsWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _revenueReadOnly = revenueReadOnly;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var barber = await _revenueReadOnly.GetById(loggedUser, id);

        if(barber is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BARBER_NOT_FOUND);
        }

        await _repository.Delete(id);

        await _unitOfWork.Commit();

    }
}
