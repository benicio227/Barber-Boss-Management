using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Repositories.Revenues;
using Moq;

namespace CommonTestUtilities.Repositories;
public class RevenuesUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IRevenueUpdateOnlyRepository> _repository;

    public RevenuesUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IRevenueUpdateOnlyRepository>();
    }

    public RevenuesUpdateOnlyRepositoryBuilder GetById(User user, Revenue? revenue)
    {
        if (revenue is not null)
        {
            _repository.Setup(repository => repository.GetById(user, revenue.Id)).ReturnsAsync(revenue);
        }

        return this;
    }

    public IRevenueUpdateOnlyRepository Build() => _repository.Object;
}
