using BarberBossManagement.Domain.Repositories.Revenues;
using Moq;

namespace CommonTestUtilities.Repositories;
public class RevenuesWriteOnlyRepositoryBuilder
{
    public static IRevenuesWriteOnlyRepository Build()
    {
        var mock = new Mock<IRevenuesWriteOnlyRepository>();

        return mock.Object;
    }
}
