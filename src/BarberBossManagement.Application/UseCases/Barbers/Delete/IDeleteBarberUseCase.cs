namespace BarberBossManagement.Application.UseCases.Barbers.Delete;
public interface IDeleteBarberUseCase
{
    Task Execute(long id);
}
