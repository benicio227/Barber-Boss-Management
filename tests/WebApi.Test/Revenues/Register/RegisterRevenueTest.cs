using BarberBossManagement.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Revenues.Register;
public class RegisterRevenueTest : BarberBossManagementClassFixture
{
    private const string METHOD = "api/Revenues";

    private readonly string _token;

    public RegisterRevenueTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]

    public async Task Success()
    {
        var request = RequestRegisterRevenueJsonBuilder.Build();


        var result = await DoPost(requestUri: METHOD, request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("title").GetString().Should().Be(request.Title);

    }

    [Theory]
    [InlineData("pt-BR")]
    public async Task Error_Title_Empty(string cultureInfo)
    {
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Title = string.Empty;

        var result = await DoPost(requestUri: METHOD, request: request, token: _token, culture: cultureInfo);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("THE_TITLE_IS_ REQUIRED", new CultureInfo(cultureInfo));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }
}
