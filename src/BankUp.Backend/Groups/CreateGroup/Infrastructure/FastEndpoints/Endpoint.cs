using BankUp.Backend.Groups.Infrastructure;
using BankUp.Backend.Infrastructure;
using FastEndpoints;
using MediatR;
using Monads;

// ReSharper disable once UnusedType.Global
namespace BankUp.Backend.Groups.CreateGroup.Infrastructure.FastEndpoints;

[HttpPut(ApiUris.Groups)]
public class Endpoint : Endpoint<Request, Response>
{
    private readonly ISender _sender;
    public Endpoint(ISender sender) => _sender = sender;

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken) =>
        await (await _sender.Send(new CreateGroupCommand(request.GroupName, request.Members,
                new List<User> { new(request.UserId, request.UserName, request.Email) }), cancellationToken))
            .Map(HandleResult);

    private Task HandleResult(Operation<Group> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(Group group) => SendAsync(new OkResponse { GroupId = group.Id }, 201);
    private Task OnKoResult(OperationError error) => SendAsync(new KoResponse{ Error = error.ToString() }, 500);
}