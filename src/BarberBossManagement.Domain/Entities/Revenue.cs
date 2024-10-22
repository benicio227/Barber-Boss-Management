using BarberBossManagement.Domain.Enums;

namespace BarberBossManagement.Domain.Entities;
public class Revenue
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }

    public long BarberShopId { get; set; } 
    public BarberShop BarberShop { get; set; } = default!;

    public long UserId { get; set; } 
    public User User { get; set; } = default!;
}
