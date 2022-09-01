using MediatR;
using Monads;

namespace BankUp.Backend.Groups.CreateGroup;

// ReSharper disable once UnusedType.Global
public class GroupCreator : IUseCase<CreateGroupCommand, Group>, IRequestHandler<CreateGroupCommand, Operation<Group>>
{
    private readonly IGroupRepository _groupRepository;

    public GroupCreator(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    public Task<Operation<Group>> Handle(CreateGroupCommand request, CancellationToken _) =>
        Execute(request);
    
    public async Task<Operation<Group>> Execute(CreateGroupCommand input) =>
        (await GroupCreated
            .Create(input.GroupName, input.Members,input.Owners)
            .Map(@event => _groupRepository.Store(new Group(@event))))
        .UnWrap();
}