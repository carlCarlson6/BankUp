using System.Net.Http.Json;
using System.Threading.Tasks;
using BankUp.Backend.Health;
using BankUp.Backend.Infrastructure;
using Microsoft.AspNetCore.TestHost;
using FluentAssertions;
using Xunit;

namespace BankUp.Backend.Test.Api.Health;

public class HealthEndpointTestsWithRavenDb : BaseApiTest
{
    [Fact]
    public async Task GivenApi_WhenGetHealthEndpoint_ThenReturnsHelloWorldResponse()
    {
        var response = await GivenTestHost().GetTestClient().GetFromJsonAsync<Response>(ApiUris.Health);
        response.Should().BeEquivalentTo(new Response());
    }
}