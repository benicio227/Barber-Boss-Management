using BarberBossManagement.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();

        _mock.Setup(passordEncripter => passordEncripter.Encrypt(It.IsAny<string>())).Returns("!#$df453");
    }

    public PasswordEncripterBuilder Verify(string password)
    {
        if(string.IsNullOrWhiteSpace(password) == false)
        {
            _mock.Setup(passwordEncripter => passwordEncripter.Verify(password, It.IsAny<string>())).Returns(true);

        }


        return this;
    }


    public IPasswordEncripter Build()
    {
        return _mock.Object;
    }
}
