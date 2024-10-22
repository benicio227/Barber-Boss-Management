using System.Net;

namespace BarberBossManagement.Exception.ExceptionBase;
public class NotFoundException : BarberBossManagementException
{
    public NotFoundException(string message) : base(message)
    {
        
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return new List<string>() { Message };
    }
}
