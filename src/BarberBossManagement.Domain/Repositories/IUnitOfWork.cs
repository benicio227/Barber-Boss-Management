namespace BarberBossManagement.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
