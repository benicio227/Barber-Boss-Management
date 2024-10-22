using BarberBossManagement.Domain.Enums;

namespace BarberBossManagement.Domain.Entities;
public class User
{
    public long Id {  get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password {  get; set; } = string.Empty;
    public Guid UserIdentifier { get; set; }
    public string Role { get; set; } = Roles.TEAM_MEMBER;

    public List<BarberShop> BarberShops { get; set; } = new List<BarberShop>();
    // Essa listta de BarberShop na entidade User existe para modelar a relação de  "um para muitos" entre
    // o usuário e as barbearias. Isso significa que um usuário pode ser dono ou responsável por várias
    // barbearias
}
