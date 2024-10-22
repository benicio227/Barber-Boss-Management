using BarberBossManagement.Domain.Entities;

namespace WebApi.Test.Resources;
public class RevenueIdentityManager
{
    private readonly Revenue _revenue;

    public RevenueIdentityManager(Revenue revenue)
    {
        _revenue = revenue;
    }

    public long GetRevenueId() => _revenue.Id;
}
