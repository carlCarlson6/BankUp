namespace BankUp.Backend.Groups.RenameGroup.Infrastructure;

public class Response { }

public class OkResponse : Response
{
    public GroupResumeView Group { get; set; }
}

public class KoResponse : Response
{
    public string Error { get; set; }
}
