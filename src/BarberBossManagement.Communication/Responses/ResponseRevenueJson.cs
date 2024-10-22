using BarberBossManagement.Communication.Enums;

namespace BarberBossManagement.Communication.Responses;
public class ResponseRevenueJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }

    public ResponseBarberShopJson BarberShop { get; set; } = default!;
}
