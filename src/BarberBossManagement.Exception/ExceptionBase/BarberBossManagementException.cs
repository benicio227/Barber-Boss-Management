namespace BarberBossManagement.Exception.ExceptionBase;
public abstract class BarberBossManagementException : SystemException
{
    protected BarberBossManagementException(string message) : base(message)
    {
        
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
