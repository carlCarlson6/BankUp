using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankUp.Backend.Groups;
using BankUp.Backend.Groups.CreateGroup;
using BankUp.Backend.Groups.RenameGroup;
using BankUp.Backend.Infrastructure;
using FluentAssertions;
using Xunit;
using OkResponse = BankUp.Backend.Groups.GetResume.OkResponse;

namespace BankUp.Backend.Test.Api.Groups;

public class GetResumeTests : BaseApiTest
{
    [Fact]
    public async Task GivenGroup_WhenGetResume_ThenReturnsResumeView()
    {
        var group = await new GroupBuilder()
            .WithEvents(
                new GroupCreated(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "this is an old name",
                    new List<Member> { new Member(Guid.NewGuid(), "memeber1") },
                    new List<User> { TestUsers.TestUser1 },
                    DateTime.UtcNow),
                new GroupRenamed(
                    Guid.NewGuid(),
                    "this is the group name",
                    DateTime.UtcNow))
            .Build(MongoDbConnectionString);
        
        var httpResponse = await GivenTestHost()
            .GetTestClient(TestUsers.TestUser1)
            .GetAsync($"{ApiUris.Groups}/{group.Id}");
        
        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        var response = await httpResponse.Content.ReadFromJsonAsync<OkResponse>();
        response!.Group.Should().BeEquivalentTo(GroupResumeView.BuildView(group.Events));
    }
}