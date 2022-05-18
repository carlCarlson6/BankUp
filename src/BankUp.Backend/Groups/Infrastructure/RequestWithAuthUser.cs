using System.Security.Claims;
using FastEndpoints;

namespace BankUp.Backend.Groups.Infrastructure;

public class RequestWithAuthUser
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; set; }
    
    [FromClaim(ClaimTypes.Name)]
    public string UserName { get; set; }
    
    [FromClaim(ClaimTypes.Email)]
    public string Email { get; set; }
}

public static class RequestWithAuthUserExtensions
{
    public static User GetUser(this RequestWithAuthUser request) => new(request.UserId, request.UserName, request.Email);
}