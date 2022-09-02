using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankUp.Backend.Groups.CreateGroup.Infrastructure.FastEndpoints;
using BankUp.Backend.Infrastructure;
using FluentAssertions;
using Xunit;

namespace BankUp.Backend.Test.Api.Groups;

public class CreateGroupTest : BaseApiTest
{
    [Fact]
    public async Task GivenTestUser1_WhenCreateGroup_ThenReturnsCreatedAndGroupId()
    {
        var httpResponse = await GivenTestHost()
            .GetTestClient(TestUsers.TestUser1)
            .PutAsJsonAsync(ApiUris.Groups, new Request
            {
                GroupName = "test-group-name",
                Members = new List<string> { TestUsers.TestUser1.Name }
            });

        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        httpResponse.StatusCode.ToString().Should().Be("Created");
        
        var response = await httpResponse.Content.ReadFromJsonAsync<OkResponse>();
        response!.GroupId.Should().NotBeEmpty();
    }
}