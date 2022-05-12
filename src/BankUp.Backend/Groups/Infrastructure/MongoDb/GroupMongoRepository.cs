using Monads;
using MongoDB.Driver;

namespace BankUp.Backend.Groups.Infrastructure.MongoDb;

public class GroupMongoRepository : IGroupRepository
{
    private readonly IMongoCollection<GroupDocument> _collection;
    
    public GroupMongoRepository(IMongoCollection<GroupDocument> collection) => _collection = collection;

    public async Task<Result<Group>> Store(Group group)
    {
        try
        {
            var document = group.ToDocument();
            var dbOperationResult = await _collection.ReplaceOneAsync(x => x.Id == document.Id, document, new ReplaceOptions{IsUpsert = true});
            return !dbOperationResult.IsAcknowledged 
                ? Result<Group>.Ko(new Error("DB ERROR")) 
                : Result<Group>.Ok(group);
        }
        catch (Exception exception)
        {
            return Result<Group>.Ko(new Error("DB ERROR", exception));
        }
    }

    public async Task<Maybe<Group>> Read(Guid groupId)
    {
        try
        {
            var groupDocument = await _collection
                .Find(document => document.Id == groupId.ToString())
                .FirstOrDefaultAsync();
            return groupDocument is null ? Maybe<Group>.None() : Maybe<Group>.Some(groupDocument.ToDomain());
        }
        catch
        {
            return Maybe<Group>.None();
        }
    }
}