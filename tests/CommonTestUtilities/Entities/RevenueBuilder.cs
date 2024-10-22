using BarberBossManagement.Domain.Entities;
using BarberBossManagement.Domain.Enums;
using Bogus;

namespace CommonTestUtilities.Entities;
public class RevenueBuilder
{
    public static List<Revenue> Collection(User user, uint count = 2)
    {
        var list = new List<Revenue>();

        if (count == 0)
            count = 1;

        var revenueId = 1;

        for(int i = 0; i < count; i++)
        {
            var revenue = Build(user);
            revenue.Id = revenueId++;

            list.Add(revenue);
        }

        return list;
    }

    public static Revenue Build(User user)
    {
        return new Faker<Revenue>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.UserId, _ => user.Id);
    }
}
