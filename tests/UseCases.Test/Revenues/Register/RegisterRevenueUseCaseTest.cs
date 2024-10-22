using BarberBossManagement.Application.UseCases.Revenues.Register;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.Revenues.Register;
public class RegisterRevenueUseCaseTest
{
    [Fact]

    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
    }

    [Fact]

    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.THE_TITLE_IS__REQUIRED));
    }


    private RegisterRevenueUseCase CreateUseCase(BarberBossManagement.Domain.Entities.User user)
    {
        var repository = RevenuesWriteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new RegisterRevenueUseCase(repository, unitOfWork, mapper, loggedUser);
    }
}
