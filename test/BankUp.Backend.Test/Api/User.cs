using System;
using System.Collections.Generic;
using BankUp.Backend.Groups;

namespace BankUp.Backend.Test.Api;

public static class TestUsers
{
    public static User TestUser1 = new(Guid.Parse("3F2504E0-4F89-11D3-9A0C-0305E82C3301"), nameof(TestUser1), $"{nameof(TestUser1)}@some_email_provider.com");
    public static User TestUser2 = new(Guid.Parse("48af44d6-8257-46c8-af6d-501735239c48"), nameof(TestUser2), $"{nameof(TestUser2)}@some_email_provider.com");

    public static IEnumerable<User> All => new List<User> { TestUser1, TestUser2 };
}