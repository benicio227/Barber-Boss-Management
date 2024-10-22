using BarberBossManagement.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Revenues.Update;
public class UpdateRevenueTest : BarberBossManagementClassFixture
{
    private const string METHOD = "api/Revenues";

    private readonly string _token;
    private readonly long _revenueId;

    public UpdateRevenueTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _revenueId = webApplicationFactory.RevenueManager.GetRevenueId();
    }

    [Fact]

    public async Task Success()
    {
        var request = RequestRegisterRevenueJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{METHOD}/{_revenueId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [InlineData("fr")]
    public async Task Error_Title_Empty(string culture)
    {
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Title = string.Empty;

        var result = await DoPut(requestUri: $"{METHOD}/{_revenueId}", request: request, token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("THE_TITLE_IS_ REQUIRED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }

    [Theory]
    [InlineData("fr")]
    public async Task Error_Revenue_Not_Found(string culture)
    {
        var request = RequestRegisterRevenueJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{METHOD}/1000", request: request, token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("REVENUE_NOT_FOUND", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }
}
