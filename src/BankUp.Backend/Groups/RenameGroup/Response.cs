namespace BankUp.Backend.Groups.RenameGroup;

public class Response { }

public class OkResponse : Response
{
    public GroupResumeView Group { get; set; }
}

public class KoResponse : Response
{
    public string Error { get; set; }
}
