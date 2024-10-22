using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.Revenues;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Revenues.Delete;
public class DeleteRevenueUseCase : IDeleteRevenueUseCase
{
    private readonly IRevenuesReadOnlyRepository _revenueReadOnly;
    private readonly IRevenuesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    public DeleteRevenueUseCase(
        IRevenuesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IRevenuesReadOnlyRepository revenueReadOnly)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _revenueReadOnly = revenueReadOnly;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var revenue = await _revenueReadOnly.GetById(loggedUser, id);

        if (revenue is null)
        {
            throw new NotFoundException(ResourceErrorMessages.REVENUE_NOT_FOUND);
        }

        await _repository.Delete(id);

        await _unitOfWork.Commit();
    }
}
