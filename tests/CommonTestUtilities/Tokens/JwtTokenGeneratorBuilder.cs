using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Tokens;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(accessTokenGenerator => accessTokenGenerator.Generate(It.IsAny<User>())).Returns("token");

        return mock.Object;
    }
}
