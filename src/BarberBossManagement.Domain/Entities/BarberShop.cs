namespace BarberBossManagement.Domain.Entities;
public class BarberShop
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address {  get; set; } = string.Empty;    
    public string PhoneNumber {  get; set; } = string.Empty;

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public List<Revenue> Revenues { get; set; } = new List<Revenue>();
}
