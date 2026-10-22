using FluentAssertions;
using PensionBridge.Domain.Batches;
using PensionBridge.Domain.Common;
using Xunit;

namespace PensionBridge.Domain.Tests;

public class ContributionBatchTests
{
    [Fact]
    public void ContributionBatch_WithValidParameters_ShouldCreate()
    {
        var batch = new ContributionBatch(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "2024-01",
            "test.csv",
            "hash123",
            "key123",
            new Money(1000m, "USD"));
        
        batch.Status.Should().Be(BatchStatus.Received);
        batch.TotalAmount.Amount.Should().Be(1000m);
    }

    [Fact]
    public void ContributionBatch_StartValidation_ShouldTransition()
    {
        var batch = new ContributionBatch(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "2024-01",
            "test.csv",
            "hash123",
            "key123",
            new Money(1000m, "USD"));
        
        batch.StartValidation();
        batch.Status.Should().Be(BatchStatus.Validating);
    }

    [Fact]
    public void ContributionBatch_CompleteValidation_WithNoErrors_ShouldBeReadyForApproval()
    {
        var batch = new ContributionBatch(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "2024-01",
            "test.csv",
            "hash123",
            "key123",
            new Money(1000m, "USD"));
        
        batch.StartValidation();
        batch.CompleteValidation(10, 10, 0);
        batch.Status.Should().Be(BatchStatus.ReadyForApproval);
    }

    [Fact]
    public void ContributionBatch_CompleteValidation_WithErrors_ShouldBeReconciliationPending()
    {
        var batch = new ContributionBatch(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "2024-01",
            "test.csv",
            "hash123",
            "key123",
            new Money(1000m, "USD"));
        
        batch.StartValidation();
        batch.CompleteValidation(10, 8, 2);
        batch.Status.Should().Be(BatchStatus.ReconciliationPending);
    }

    [Fact]
    public void ContributionBatch_Approve_ShouldTransition()
    {
        var batch = new ContributionBatch(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "2024-01",
            "test.csv",
            "hash123",
            "key123",
            new Money(1000m, "USD"));
        
        batch.StartValidation();
        batch.CompleteValidation(10, 10, 0);
        batch.Approve();
        batch.Status.Should().Be(BatchStatus.Approved);
    }
}
