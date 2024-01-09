using System.Globalization;
using System.Text;
using Ecocell.Communication.Request.UserApp;
using Ecocell.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace WebApi.Test.V1;

public class ControllerBase : IClassFixture<EcocellWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ControllerBase(EcocellWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();

        var configuration = factory.Services.GetRequiredService<IConfiguration>();

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configuration.GetRequiredSection("Configurations:ApiIdentity").Value);
    }

    protected async Task<HttpResponseMessage> PostRequest(string method, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PostAsync(method, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }
}