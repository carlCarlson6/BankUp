using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankUp.Backend.Groups;
using BankUp.Backend.Groups.CreateGroup;
using BankUp.Backend.Infrastructure;
using FluentAssertions;
using Xunit;
using OkResponse = BankUp.Backend.Groups.RenameGroup.Infrastructure.OkResponse;
using Request = BankUp.Backend.Groups.RenameGroup.Infrastructure.Request;

namespace BankUp.Backend.Test.Api.Groups;

public class RenameGroupTests : BaseApiTest
{
    [Fact]
    public async Task GivenGroup_WhenTestUser1RenamesTheGroup_ThenResumeViewWithUpdatedNameIsReturned()
    {
        var group = await new GroupBuilder()
            .WithEvents(new GroupCreated(
                Guid.NewGuid(), 
                Guid.NewGuid(), 
                "this is an old name", 
                new List<Member> { new Member(Guid.NewGuid(), "memeber1") },
                new List<User> { TestUsers.TestUser1 }, 
                DateTime.UtcNow))
            .Build(MongoDbConnectionString);

        const string newName = "this is a new name"; 
        
        var httpResponse = await GivenTestHost()
            .GetTestClient(TestUsers.TestUser1)
            .PutAsJsonAsync($"{ApiUris.Groups}/{group.Id}/rename", new Request{ GroupName = newName });

        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        var response = await httpResponse.Content.ReadFromJsonAsync<OkResponse>();
        response!.Group.Name.Should().Be(newName);
    }
}