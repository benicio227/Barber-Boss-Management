using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;//Define as informações sobre o usuário que serão incluídas no token, chamas de Claims
using System.Text;

namespace BarberBossManagement.Infrastructure.Tokens;
internal class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes; // Define por quanto tempo o token será válido, em minutos
    private readonly string _signingKey; // Chave usada para assinar o token, garantindo sua autenticidade

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;   
    }
    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name), //Armazena o nome do usuário no token
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()) //Armazena um identificador do usuario
        }; // Essas informações(claims) serão verificadas quando o token for usado para autenticação.

        var tokenDescriptor = new SecurityTokenDescriptor // É configurado para descrever o conteúdo do token
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(Security(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims) // As claims(informações do usuário) associadas ao token
        };

        var tokenHandler = new JwtSecurityTokenHandler(); // É a classe usada para gerar o token

        var securityToken = tokenHandler.CreateToken(tokenDescriptor); // Usa o tokenDescriptor para criar o token com as informações definidas

        return tokenHandler.WriteToken(securityToken);
        // O método WriteToken converte o token para uma string legível, que é o formato que será retornado
        // e usado para autenticação no sistema.
    }

    private SymmetricSecurityKey Security()
    //Esse método retorna uma chave de segurança  simétrica(SymmetricSecurityKey), que é usada para assinar
    // o token
    // A chave é criada a partir da signinKey que você passou no construtor. Basicamente, isso transforma
    // sua chave secreta em um formato que o .NET pode usar para criptografia
    {
        var key = Encoding.UTF8.GetBytes(_signingKey); // O método GetBytes pega a string e converte cada caractere
        // dessa string em um valor numérico(bytes) conforme a codificação UTTF-8

        return new SymmetricSecurityKey(key);
    }
}
