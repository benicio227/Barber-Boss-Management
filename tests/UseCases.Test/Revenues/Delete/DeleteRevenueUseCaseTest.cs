using BarberBossManagement.Application.UseCases.Revenues.Delete;
using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Revenues.Delete;
public class DeleteRevenueUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var revenue = RevenueBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, revenue);

        var act = async () => await useCase.Execute(revenue.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Revenue_Not_Found()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.REVENUE_NOT_FOUND));
    }


    private DeleteRevenueUseCase CreateUseCase(User user, Revenue? revenue = null)
    {
        var repositoryWriteOnly = RevenuesWriteOnlyRepositoryBuilder.Build();
        var repository = new RevenuesReadOnlyRepositoryBuilder().GetById(user, revenue).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new DeleteRevenueUseCase(repositoryWriteOnly, unitOfWork, loggedUser, repository);
    }
}
