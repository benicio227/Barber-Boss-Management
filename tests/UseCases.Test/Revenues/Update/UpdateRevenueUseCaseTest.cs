using BarberBossManagement.Application.UseCases.Revenues.Update;
using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Exception.ExceptionBase;
using BarberBossManagement.Exception;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Test.Revenues.Update;
public class UpdateRevenueUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        var revenue = RevenueBuilder.Build(loggedUser); 

        var useCase = CreateUseCase(loggedUser, revenue);

        var act = async () => await useCase.Execute(revenue.Id, request);

        await act.Should().NotThrowAsync();

        revenue.Title.Should().Be(request.Title);
        revenue.Description.Should().Be(request.Description);
        revenue.Date.Should().Be(request.Date);
        revenue.Amount.Should().Be(request.Amount);
        revenue.PaymentType.Should().Be((BarberBossManagement.Domain.Enums.PaymentType)request.PaymentType);
    }


    [Fact]
    public async Task Error_title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var revenue = RevenueBuilder.Build(loggedUser);

        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser, revenue);

        var act = async () => await useCase.Execute(revenue.Id, request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.THE_TITLE_IS__REQUIRED));
    }


    [Fact]
    public async Task Error_Revenue_Not_Found()
    {
        var loggedUser = UserBuilder.Build();

        var request = RequestRegisterRevenueJsonBuilder.Build();
     

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000, request);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.REVENUE_NOT_FOUND));
    }


    private UpdateRevenueUseCase CreateUseCase(User user, Revenue? revenue = null)
    {
        var repository = new RevenuesUpdateOnlyRepositoryBuilder().GetById(user, revenue).Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new UpdateRevenueUseCase(mapper, unitOfWork, repository, loggedUser);
    }
}
