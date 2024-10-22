using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Repositories.Revenues;
using Moq;

namespace CommonTestUtilities.Repositories;
public class RevenuesReadOnlyRepositoryBuilder
{
    private readonly Mock<IRevenuesReadOnlyRepository> _repository;

    public RevenuesReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IRevenuesReadOnlyRepository>();
    }

    public RevenuesReadOnlyRepositoryBuilder GetAll(User user, List<Revenue> revenues)
    {
        _repository.Setup(repository => repository.GetAll(user)).ReturnsAsync(revenues);

        return this;
    }

    public RevenuesReadOnlyRepositoryBuilder GetById(User user, Revenue? revenue)
    {
        if(revenue is not null)
        {
            _repository.Setup(repository => repository.GetById(user, revenue.Id)).ReturnsAsync(revenue);
        }

        return this;
    }

    public IRevenuesReadOnlyRepository Build()
    {
        return _repository.Object;
    }

}
