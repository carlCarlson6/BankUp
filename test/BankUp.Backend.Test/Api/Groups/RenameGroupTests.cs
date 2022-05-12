using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankUp.Backend.Groups;
using BankUp.Backend.Groups.RenameGroup;
using BankUp.Backend.Infrastructure;
using FluentAssertions;
using Xunit;

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
                new List<Guid> { TestUsers.TestUser1.Id }, 
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