using AutoMapper;
using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Repositories;
using BarberBossManagement.Domain.Repositories.Revenues;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Exception.ExceptionBase;

namespace BarberBossManagement.Application.UseCases.Revenues.Register;
public class RegisterRevenueUseCase : IRegisterRevenueUseCase // A classe RegisterRevenueUseCase implementa a interface IRegisterRevenueUseCase
{
    private readonly IRevenuesWriteOnlyRepository _repository; // Esse repositório lida com operações de escrita para os faturamentos
    private readonly IUnitOfWork _unitOfWork; // Unidade de trabalho que gerencia as transações com o banco de dados
    private readonly IMapper _mapper; // Um objeto da biblioteca AutoMapper usado para mapear entre diferentes objetos
    private readonly ILoggedUser _loggedUser; // Serviço que fornece informações sobre o usuário atualmente logado
    public RegisterRevenueUseCase( // O construtor recebe as dependências necessárias através da injeção de dependência e as inicializa
        IRevenuesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository; 
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseRegisteredRevenueJson> Execute(RequestRevenueJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get(); // Obtém as informações do usuario logado. Isso é importante para associar a receita ao usuario que esta fazendo o registro.

        var revenue = _mapper.Map<Revenue>(request); // Mapeia os dados de RequestRevenueJson para uma nova instância da entidade Revenue 

        revenue.UserId = loggedUser.Id; // Define o UserId da receita para o ID do Usuário logado

        await _repository.Add(revenue); // Adiciona a receita ao repositório

        await _unitOfWork.Commit(); // Persiste as mudanças no banco de dados através da unidade de trabalho


        return _mapper.Map<ResponseRegisteredRevenueJson>(revenue);
        // Retorna uma resposta mapeada (ResponseRegisteredRevenueJson) com os dados da receita registrada.
    }

    public void Validate(RequestRevenueJson request)
    {
        var validator = new RevenueValidator(); //Usa um validador(RevenueValidator) para validar os dados do pedido

        var result = validator.Validate(request); //Obtém os resultados da validação

        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

        if (result.IsValid is false) // Se a validação falhar, lança uma exceção com as mensagens de erro.
        {
            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
