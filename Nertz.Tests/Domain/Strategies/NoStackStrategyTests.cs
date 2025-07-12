using Nertz.Domain.Strategies;
using Nertz.Domain.ValueObjects;
using Shouldly;

namespace Nertz.Tests.Domain.Strategies;

public class NoStackStrategyTests
{
    [Fact]
    public void Should_Not_Stack()
    {
        var sut = new NoStackStrategy();

        var wasSuccess = sut.CanStack(new Card {}, new Card {});
        
        wasSuccess.ShouldBe(false);
    }
}