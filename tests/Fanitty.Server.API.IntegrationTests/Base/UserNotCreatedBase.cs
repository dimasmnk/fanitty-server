using Fanitty.Server.API.IntegrationTests.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;

namespace Fanitty.Server.API.IntegrationTests.Base;

public class UserNotCreatedBase
{
    protected TestWebApplicationFactory<Program> webApplicationFactory;
    protected HttpClient httpClient;

    public UserNotCreatedBase()
    {
        webApplicationFactory = new TestWebApplicationFactory<Program>(Guid.NewGuid().ToString());
        httpClient = new HttpClientFactory(webApplicationFactory).CreateAuthenticatedWithoutUserHttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, TokenGenerator.GenerateToken());
    }
}
