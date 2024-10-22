namespace BarberBossManagement.Communication.Responses;
public class ResponseBarberShopJson
{
    public long Id {  get; set; }
    public string Name {  get; set; } = string.Empty;
    public string Address {  get; set; } = string.Empty;
    public string PhoneNumber {  get; set; } = string.Empty;
    public long UserId { get; set; }

    public List<ResponseShortRevenueJson> Revenues { get; set; } = new List<ResponseShortRevenueJson>();
}
