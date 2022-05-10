using System;
using System.Collections.Generic;

namespace BankUp.Backend.Test.Api;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class TestUsers
{
    public static User TestUser1 = new() { Id = Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"), Name = nameof(TestUser1) };

    public static IEnumerable<User> All => new List<User> { TestUser1 };
}