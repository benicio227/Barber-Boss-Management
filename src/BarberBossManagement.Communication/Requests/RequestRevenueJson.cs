using BarberBossManagement.Communication.Enums;

namespace BarberBossManagement.Communication.Requests;
public class RequestRevenueJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long BarberShopId {  get; set; }
    //public long UserId {  get; set; }
}
