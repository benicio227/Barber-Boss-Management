using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.User;
using BarberBossManagement.Domain.Security.Cryptography;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;
using FluentValidation.Results;

namespace BarberBossManagement.Application.UseCases.Users.ChangePassword;
public class ChangePasswordUserCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUserCase(
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter)
    {
        _loggedUser = loggedUser;
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();

        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id);
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        var passwordMatch = _passwordEncripter.Verify(request.Password, loggedUser.Password);

        if (passwordMatch is false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
        }

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
