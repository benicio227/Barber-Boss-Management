﻿using BarberBossManagement.Communication.Enums;
using BarberBossManagement.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestRegisterRevenueJsonBuilder
{
    public static RequestRevenueJson Build()
    {
        return new Faker<RequestRevenueJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
    }
}
