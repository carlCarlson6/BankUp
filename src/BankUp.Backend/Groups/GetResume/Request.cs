using BankUp.Backend.Groups.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BankUp.Backend.Groups.GetResume;

public class Request : RequestWithAuthUser
{
    [FromRoute]
    public Guid GroupId { get; set; }
}