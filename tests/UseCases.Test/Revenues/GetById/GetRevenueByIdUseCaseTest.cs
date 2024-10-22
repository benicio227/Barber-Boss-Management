using BarberBossManagement.Application.UseCases.Revenues.GetAll;
using BarberBossManagement.Application.UseCases.Revenues.GetById;
using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;


namespace UseCases.Test.Revenues.GetById;
public class GetRevenueByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var revenue = RevenueBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, revenue);

        var result = await useCase.Execute(revenue.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(revenue.Id);
        result.Title.Should().Be(revenue.Title);
        result.Description.Should().Be(revenue.Description);
        result.Date.Should().Be(revenue.Date);
        result.Amount.Should().Be(revenue.Amount);
        result.PaymentType.Should().Be((BarberBossManagement.Communication.Enums.PaymentType)revenue.PaymentType);
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

    private GetRevenueByIdUseCase CreateUseCase(User user, Revenue? revenue = null)
    {
        var repository = new RevenuesReadOnlyRepositoryBuilder().GetById(user, revenue).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetRevenueByIdUseCase(repository, mapper, loggedUser);
    }
}
