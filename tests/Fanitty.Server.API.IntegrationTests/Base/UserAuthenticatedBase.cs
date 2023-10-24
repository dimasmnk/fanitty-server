using Fanitty.Server.API.IntegrationTests.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;

namespace Fanitty.Server.API.IntegrationTests.Base;

public class UserAuthenticatedBase
{
    protected TestWebApplicationFactory<Program> webApplicationFactory;
    protected HttpClient httpClient;

    public UserAuthenticatedBase()
    {
        webApplicationFactory = new TestWebApplicationFactory<Program>(Guid.NewGuid().ToString());
        httpClient = new HttpClientFactory(webApplicationFactory).CreateAuthenticatedUserHttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, TokenGenerator.GenerateToken());
    }
}