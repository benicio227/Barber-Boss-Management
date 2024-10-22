using BarberBossManagement.Exception;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Revenues.Delete;
public class DeleteRevenueTest : BarberBossManagementClassFixture
{
    private const string METHOD = "api/Revenues";

    private readonly string _token;
    private readonly long _revenueId;

    public DeleteRevenueTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _revenueId = webApplicationFactory.RevenueManager.GetRevenueId();
    }

    [Fact]

    public async Task Success()
    {
 
        var result = await DoDelete(requestUri: $"{ METHOD}/{_revenueId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);

        result = await DoGet(requestUri: $"{METHOD}/{_revenueId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [InlineData("pt-BR")]
    public async Task Error_Revenue_Not_Found(string cultureInfo)
    {

        var result = await DoDelete(requestUri: $"{METHOD}/1000", token: _token, culture: cultureInfo);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("REVENUE_NOT_FOUND", new CultureInfo(cultureInfo));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }
}
