namespace BankUp.Backend.Groups.CreateGroup.Infrastructure.FastEndpoints;

public class Response { }

public class OkResponse : Response
{
    public Guid GroupId { get; set; }
}

public class KoResponse : Response
{
    public string Error { get; set; }
}