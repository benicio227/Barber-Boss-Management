using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Security.Tokens;
using BarberBossManagement.Domain.Services.LoggedUser;
using BarberBossManagement.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberBossManagement.Infrastructure.Services.LoggedUser;
internal class LoggedUser : ILoggedUser
//Esse código implementa a classe LoggedUser, que é responsável por obter o usuário logado em uma aplicação
// usando o token JWT fornecido na requisição HTTP
{
    private readonly BarberBossManagementDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider; // Essa interface é responsavel por fornecer o token JWT
    // da requisição HTTP atual. A implementação dessa interface extrai o token do cabeçalho da requisição HTTP
    public LoggedUser(BarberBossManagementDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }
    public async Task<User> Get() 
    // Esse método é responsável por recuperar as informações do usuário logado a partir do token fornecido
    // na requisição HTTP
    {
        string token = _tokenProvider.TokenOnRequest();
        // Aqui o Token JWT é extraido da requisição Http usando o método TokenOnRequest() do ITokenProvider.
        // Esse token contem todas as informações do usuário autenticado

        var tokenHandler = new JwtSecurityTokenHandler(); // Essa classe é criada para ler e manipular o token JWT

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        // O token JWT é decodificado usando o método ReadJwtToken(token) que retorna um objeto JwtSecurityToken,
        // contendo as "claims"(informações associadas ao token, como nome, ID etc)

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
        // O método busca a "claim" que contém o ID do usuário(ClaimTypes.Sid) que foi inserida no token
        // quando ele foi gerado
        // O valor dessa claim é o identificador único do usuário(identifier), que será usado para buscar o
        // usuário no banco de dados.

        return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
        //retorna o objeto User correspondente ao ID
    }
}
