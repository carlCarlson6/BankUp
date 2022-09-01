using BankUp.Backend.Groups.Infrastructure;
using BankUp.Backend.Infrastructure;
using FastEndpoints;
using Monads;

// ReSharper disable once UnusedType.Global
namespace BankUp.Backend.Groups.CreateGroup.Infrastructure.FastEndpoints;

[HttpPut(ApiUris.Groups)]
public class Endpoint : Endpoint<Request, Response>
{
    private readonly IGroupRepository _groupRepository;
    public Endpoint(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken) =>
        await (await GroupCreated
                .Create(request.GroupName, request.Members,new List<User> { request.GetUser() })
                .Map(@event => _groupRepository.Store(new Group(@event))))
            .UnWrap()
            .Map(HandleResult);

    private Task HandleResult(Operation<Group> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(Group group) => SendAsync(new OkResponse { GroupId = group.Id }, 201);
    private Task OnKoResult(OperationError error) => SendAsync(new KoResponse{ Error = error.ToString() }, 500);
}