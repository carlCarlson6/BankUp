using System.Security.Claims;
using FastEndpoints;

namespace BankUp.Backend.Groups.CreateGroup;

public class Request
{
    public string GroupName { get; set; }
    
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid Creator { get; set; }
}