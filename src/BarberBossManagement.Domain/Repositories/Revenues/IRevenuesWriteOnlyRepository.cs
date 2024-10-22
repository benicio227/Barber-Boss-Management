using BarberBossManagement.Domain.Entities;

namespace BarberBossManagement.Domain.Repositories.Revenues;
public interface IRevenuesWriteOnlyRepository // É uma interface que define os métodos que um repositório de faturamentos deve implementar
{
    Task Add(Revenue revenue); 
    // O método Add é assíncrono(indicado pela palavra-chave Task), o que significa que ele pode ser executado
    // de forma não bloqueante, permitindo que a aplicação continue a responder enquanto a operação está em
    // andamento.
    // O método recebe um objeto do tipo Revenue, que representa a receita a ser adicionada ao repositório.

    Task Delete(long id);
    // Recebe um parâmetro do tipo long, que representa o identificador(ID) da receita a ser excluida


}
