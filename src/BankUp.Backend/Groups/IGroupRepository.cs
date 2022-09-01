using Monads;

namespace BankUp.Backend.Groups;

public interface IGroupRepository
{
    public Task<Operation<Group>> Store(Group group);
    public Task<Maybe<Group>> Read(Guid groupId);
}