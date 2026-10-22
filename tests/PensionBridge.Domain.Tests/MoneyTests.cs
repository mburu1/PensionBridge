using FluentAssertions;
using PensionBridge.Domain.Common;
using Xunit;

namespace PensionBridge.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void Money_WithValidAmount_ShouldCreate()
    {
        var money = new Money(100.50m, "USD");
        money.Amount.Should().Be(100.50m);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Money_WithNegativeAmount_ShouldThrow()
    {
        Action act = () => new Money(-10m, "USD");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Money_Add_SameCurrency_ShouldSum()
    {
        var money1 = new Money(100m, "USD");
        var money2 = new Money(50m, "USD");
        var result = money1.Add(money2);
        result.Amount.Should().Be(150m);
    }

    [Fact]
    public void Money_Add_DifferentCurrency_ShouldThrow()
    {
        var money1 = new Money(100m, "USD");
        var money2 = new Money(50m, "EUR");
        Action act = () => money1.Add(money2);
        act.Should().Throw<InvalidOperationException>();
    }
}
