﻿using BarberBossManagement.Communication.Enums;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Revenues.GetById;
public class GetRevenueByIdTest : BarberBossManagementClassFixture
{
    private const string METHOD = "api/Revenues";

    private readonly string _token;
    private readonly long _revenueId;

    public GetRevenueByIdTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _revenueId = webApplicationFactory.RevenueManager.GetRevenueId();
    }

    [Fact]

    public async Task Success()
    {
        var result = await DoGet(requestUri: $"{METHOD}/{_revenueId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("id").GetInt64().Should().Be(_revenueId);
        response.RootElement.GetProperty("title").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("description").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("date").GetDateTime().Should().NotBeAfter(DateTime.Today);
        response.RootElement.GetProperty("amount").GetDecimal().Should().BeGreaterThan(0);

        var paymentType = response.RootElement.GetProperty("paymentType").GetInt32();
        Enum.IsDefined(typeof(PaymentType), paymentType).Should().BeTrue(); 

    }


}
