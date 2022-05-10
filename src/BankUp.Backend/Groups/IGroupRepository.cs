using Monads;

namespace BankUp.Backend.Groups;

public interface IGroupRepository
{
    public Task<Result<Group>> Store(Group group);
}