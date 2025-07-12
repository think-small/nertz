using Nertz.Domain.Strategies;
using Shouldly;

namespace Nertz.Tests.Domain.Strategies;

public class NoRemoveStrategyTests
{
    [Fact]
    public void Should_Not_Remove()
    {
        var sut = new NoRemoveStrategy();
        
        var wasSuccess = sut.TryRemoveAt([], 0, 1, out var transaction);

        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }
}