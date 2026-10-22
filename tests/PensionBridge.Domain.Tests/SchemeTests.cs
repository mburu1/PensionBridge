using FluentAssertions;
using PensionBridge.Domain.Schemes;
using Xunit;

namespace PensionBridge.Domain.Tests;

public class SchemeTests
{
    [Fact]
    public void Scheme_WithValidParameters_ShouldCreate()
    {
        var scheme = new Scheme(Guid.NewGuid(), "SCHEME001", "Test Scheme", "BC Company", "USD");
        scheme.Code.Should().Be("SCHEME001");
        scheme.Name.Should().Be("Test Scheme");
        scheme.Status.Should().Be(SchemeStatus.Active);
    }

    [Fact]
    public void Scheme_WithEmptyCode_ShouldThrow()
    {
        Action act = () => new Scheme(Guid.NewGuid(), "", "Test", "BC", "USD");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Scheme_UpdateName_ShouldUpdate()
    {
        var scheme = new Scheme(Guid.NewGuid(), "SCHEME001", "Test Scheme", "BC Company", "USD");
        scheme.UpdateName("Updated Name");
        scheme.Name.Should().Be("Updated Name");
        scheme.ModifiedAt.Should().NotBeNull();
    }

    [Fact]
    public void Scheme_SetStatus_ShouldUpdate()
    {
        var scheme = new Scheme(Guid.NewGuid(), "SCHEME001", "Test Scheme", "BC Company", "USD");
        scheme.SetStatus(SchemeStatus.Inactive);
        scheme.Status.Should().Be(SchemeStatus.Inactive);
    }
}
