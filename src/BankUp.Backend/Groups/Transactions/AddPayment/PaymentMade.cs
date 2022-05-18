using System.Text.Json.Serialization;

namespace BankUp.Backend.Groups.Transactions.AddPayment;

public class PaymentMade : ITransaction
{
    public Guid Id { get; }
    public string Description { get; }
    public string Concept { get; }
    public uint Amount { get; }
    public IEnumerable<User> For { get; }
    public User CreatedBy { get; }
    public DateTime CreatedAt { get; }
    
    [JsonConstructor]
    public PaymentMade(Guid id, string description, string concept, uint amount, IEnumerable<User> @for, User createdBy, DateTime createdAt) => 
        (Id, Description, Concept, Amount, For, CreatedBy, CreatedAt) = 
        (id, description, concept, amount, @for, createdBy, createdAt);

    public static PaymentMade Create(string description, string concept, uint amount, IEnumerable<User> @for, User createdBy) =>
        new(Guid.NewGuid(), description, concept, amount, @for, createdBy, DateTime.UtcNow);
}