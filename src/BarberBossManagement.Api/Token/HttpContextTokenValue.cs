using BarberBossManagement.Domain.Security.Tokens;

namespace BarberBossManagement.Api.Token;

public class HttpContextTokenValue : ITokenProvider
// Essa classe implementa a interface ITokenProvider para obter o token da requisição HTTP atual.
// Ele utliza o IHttpContextAccessor para acessar o contexto HTTP e extrair o token presente no cabeçalho da requisição
{
    private readonly IHttpContextAccessor _contextAccessor;
    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }
    public string TokenOnRequest()
    // Esse método é responsável por extrair o token JWT presente no cabeçalho da requisição
    {
        var authorizarion = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        //Acessa o valor do token que está dentro do cabeçalho Authorization

        return authorizarion["Bearer ".Length..].Trim();
    }
}
