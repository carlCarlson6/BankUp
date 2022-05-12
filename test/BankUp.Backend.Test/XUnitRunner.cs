// Dot not delete otherwise this results in bad behaviour

using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
// ReSharper disable once EmptyNamespace
namespace BankUp.Backend.Test { }