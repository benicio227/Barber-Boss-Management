using BarberBossManagement.Application.UseCases.Revenues.GetAll;
using BarberBossManagement.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Revenues.GetAll;
public class GetAllRevenueUseCaseTest
{
    [Fact]

    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var revenues = RevenueBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, revenues);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Revenues.Should().NotBeNullOrEmpty().And.AllSatisfy(revenue =>
        {
            revenue.Id.Should().BeGreaterThan(0);
            revenue.Title.Should().NotBeNullOrEmpty();
            revenue.Amount.Should().BeGreaterThan(0);
        });
    }



    private GetAllRevenueUseCase CreateUseCase(User user, List<Revenue> revenues)
    {
        var repository = new RevenuesReadOnlyRepositoryBuilder().GetAll(user, revenues).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetAllRevenueUseCase(repository, mapper, loggedUser);
    }
}
