using BarberBossManagement.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.Update;
public class UpdateUserTest : BarberBossManagementClassFixture
{
    private const string METHOD = "api/User";

    private readonly string _token;

    public UpdateUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]

    public async Task Success()
    {
        var request = RequestUpdateUserJsonBuilder.Build();

        var response = await DoPut(METHOD, request, token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [InlineData("pt-BR")]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPut(METHOD, request, token: _token, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectdMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", new CultureInfo("pt-BR"));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectdMessage));
    }
}
