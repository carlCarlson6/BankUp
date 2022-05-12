using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankUp.Backend.Groups;
using BankUp.Backend.Groups.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace BankUp.Backend.Test.Api;

public class GroupBuilder
{
    private List<IEvent> _events = new List<IEvent>();

    public GroupBuilder WithEvents(params IEvent[] events)
    {
        _events = _events.Concat(events.ToList()).ToList();
        return this;
    }
    
    public async Task<Group> Build(string connectionString)
    {
        var collection = new MongoClient(connectionString)
            .GetDatabase("BankUpDb")
            .GetCollection<GroupDocument>("groups");
        
        var group = new Group(_events);
        var document = group.ToDocument();
        
        await collection.ReplaceOneAsync(x => x.Id == document.Id, document, new ReplaceOptions{IsUpsert = true});

        return group;
    }
}