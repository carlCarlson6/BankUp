using MediatR;
using Monads;

namespace BankUp.Backend.Groups.CreateGroup;

public record CreateGroupCommand(string GroupName, List<string> Members, List<User> Owners) : IRequest<Operation<Group>>;