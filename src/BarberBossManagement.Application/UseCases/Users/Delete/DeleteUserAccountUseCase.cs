
using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.User;
using BarberBossManagement.Domain.Services.LoggedUser;

namespace BarberBossManagement.Application.UseCases.Users.Delete;
public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserAccountUseCase(
        ILoggedUser loggedUser,
        IUserWriteOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        await _repository.Delete(user);

        await _unitOfWork.Commit();
    }
}
