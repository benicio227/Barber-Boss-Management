using System.Net;

namespace BarberBossManagement.Exception.ExceptionBase;
public class InvalidLoginException : BarberBossManagementException
{
    public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
    {
        
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
