﻿using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Revenues.GetAll;
public class GetAllRevenueTest : BarberBossManagementClassFixture
{
    private const string METHOD = "api/Revenues";

    private readonly string _token;

    public GetAllRevenueTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]

    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("revenues").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
