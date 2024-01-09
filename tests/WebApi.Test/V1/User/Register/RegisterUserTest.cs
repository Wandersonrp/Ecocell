using System.Net;
using System.Text.Json;
using Ecocell.Exceptions;
using FluentAssertions;
using Utilities.Test.Request;

namespace WebApi.Test.V1.User.Register;

public class RegisterUserTest : ControllerBase
{
    private const string METHOD = "signup";

    public RegisterUserTest(EcocellWebApplicationFactory<Program> factory) : base(factory)
    {
    }   

    [Fact]
    public async Task Validate_Success()
    {
        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        
        var response = await PostRequest(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var data = await JsonDocument.ParseAsync(responseBody);

        data.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validate_Error_Empty_Name()
    {
        var request = RequestRegisterUserBuilder.NaturalPersonBuilder();
        request.Name = "";

        var response = await PostRequest(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var data = await JsonDocument.ParseAsync(responseBody);

        var errors = data.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceErrorMessage.EMPTY_USER_NAME));
    }
}