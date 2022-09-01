using Microsoft.VisualBasic;
using Monads;

namespace BankUp.Backend.Groups.CreateGroup.Errors;

public class InvalidGroupCreation : OperationError
{
    public IEnumerable<OperationError> Errors { get; }
    
    public InvalidGroupCreation(IReadOnlyCollection<OperationError> errors) : base(FormatMessage(errors)) => Errors = errors;
    public InvalidGroupCreation(params OperationError[] errors) : this(errors.ToList()) { }
    
    private static string FormatMessage(IEnumerable<OperationError> errors) =>
        $"{nameof(InvalidGroupCreation)} Errors: [{Strings.Join(errors.Select(e => e.Message).ToArray(), " - ")}]";
}