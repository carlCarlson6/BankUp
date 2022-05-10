using BankUp.Backend.Infrastructure;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace BankUp.Backend.Health;

[HttpGet(ApiUris.Health), AllowAnonymous]
public class HealthEndpoint : EndpointWithoutRequest<Response>
{
    public override Task HandleAsync(CancellationToken ct) => SendAsync(new Response(), cancellation: ct);
}

public class Response
{
    public string Message { get; set; } = "hello world! :) - i'm healthy";
}