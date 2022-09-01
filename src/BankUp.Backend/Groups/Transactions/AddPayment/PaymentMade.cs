namespace BankUp.Backend.Groups.Transactions.AddPayment;

public record PaymentMade(
    Guid Id,
    string Description,
    string Concept,
    uint Amount,
    IEnumerable<Member> PayedBy,
    IEnumerable<Member> PayedFor,
    User CreatedBy,
    DateTime CreatedAt) : ITransaction
{
    public static PaymentMade Create(string description, string concept, uint amount, IEnumerable<Member> payedBy, IEnumerable<Member> payedFor, User createdBy) =>
        new(Guid.NewGuid(), description, concept, amount, payedBy, payedFor, createdBy, DateTime.UtcNow);
}