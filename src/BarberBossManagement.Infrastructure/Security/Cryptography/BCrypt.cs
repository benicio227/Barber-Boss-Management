using BarberBossManagement.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace BarberBossManagement.Infrastructure.Security.Cryptography;
internal class BCrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    // recebe uma senha em texto puro
    // retorna o hash gerado, que será armazenado no banco de dados no lugar da senha em texto puro.
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash)
    // string password = A senha em texto puro que o usuário forneceu(por exemplo, ao tentar fazer login)
    // string passwordHash = O hash da senha que está armazenada no banco de dados
    {
        return BC.Verify(password, passwordHash); // O método verify é usado para verificar se a senha fornecida corresponde ao hash armazenado

    }
}
