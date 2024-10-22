using AutoMapper;
using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Domain.Repositories.Revenues;
using BarberBossManagement.Domain.Services.LoggedUser;

namespace BarberBossManagement.Application.UseCases.Revenues.GetAll;
public class GetAllRevenueUseCase : IGetAllRevenueUseCase
{
    private readonly IRevenuesReadOnlyRepository _repository; // Essa interface define métodos para acessar receitas de forma somente leitura
    private readonly IMapper _mapper; 
    private readonly ILoggedUser _loggedUser;
    public GetAllRevenueUseCase(
        IRevenuesReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseRevenuesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get(); // O código obtém informações sobre o usuário logado chamando
        // o método Get do serviço ILoggedUser. Este usuário será usado para filtrar os faturamentos que pertencem
        // a ele.

        var result = await _repository.GetAll(loggedUser); // O código chama o método GetAll do repositório,
        // passando o usuário logado como argumento.Esse método deve retornar uma lista de faturamentos que per
        //tencem ao usuário

        return new ResponseRevenuesJson
        {
            Revenues = _mapper.Map<List<ResponseShortRevenueJson>>(result)
            // O AutoMapper é usado para converter a lista de receitas retornadas pelo repositorio(result) em uma
            // lista de objetos do tipo ResponseShortRevenueJson.
        };
    }
}
