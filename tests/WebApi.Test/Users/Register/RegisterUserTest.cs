using BarberBossManagement.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MigraDoc.Rendering;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.Register;
public class RegisterUserTest : BarberBossManagementClassFixture 
{
    private const string METHOD = "api/User";

    private readonly string _token;
    public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await DoPost(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("pt-BR")]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await DoPost(requestUri: METHOD, request: request, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectdMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", new CultureInfo("pt-BR"));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectdMessage));
    }
}
