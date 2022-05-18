using System.Net.Http;
using BankUp.Backend.Groups;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;

namespace BankUp.Backend.Test.Api;

public static class TestingExtensions
{
    public static HttpClient GetTestClient(this IWebHost webHost, User user)
    {
        var client = webHost.GetTestClient();
        if (user is { })
            client.DefaultRequestHeaders.Add("user", user.Id.ToString());

        return client;
    }
    
    public static AuthenticationBuilder AddFakeAuthenticationHandler(this IServiceCollection services) =>
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });
}