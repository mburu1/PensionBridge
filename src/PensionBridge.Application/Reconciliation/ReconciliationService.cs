using PensionBridge.Application.Abstractions;
using PensionBridge.Domain.Batches;
using PensionBridge.Domain.Contributions;
using PensionBridge.Domain.Reconciliation;

namespace PensionBridge.Application.Reconciliation;

public class ReconciliationService : IReconciliationService
{
    private readonly IRepository<ContributionBatch> _batchRepository;
    private readonly IRepository<ContributionLine> _lineRepository;

    public ReconciliationService(
        IRepository<ContributionBatch> batchRepository,
        IRepository<ContributionLine> lineRepository)
    {
        _batchRepository = batchRepository;
        _lineRepository = lineRepository;
    }

    public async Task<IEnumerable<ReconciliationResult>> ReconcileBatchAsync(Guid batchId, CancellationToken cancellationToken = default)
    {
        var batch = await _batchRepository.GetByIdAsync(batchId, cancellationToken);
        if (batch == null)
            throw new InvalidOperationException($"Batch {batchId} not found");

        var lines = await _lineRepository.GetAllAsync(cancellationToken);
        var batchLines = lines.Where(l => l.BatchId == batchId).ToList();

        var results = new List<ReconciliationResult>();

        // Rule: Total amount reconciliation
        var lineTotal = batchLines.Sum(l => l.GetTotalContribution().Amount);
        if (Math.Abs(lineTotal - batch.TotalAmount.Amount) > 0.01m)
        {
            results.Add(new ReconciliationResult(
                Guid.NewGuid(),
                batchId,
                "TOTAL_AMOUNT_MISMATCH",
                "High",
                batch.TotalAmount.Amount,
                lineTotal,
                $"Batch total {batch.TotalAmount} differs from line sum {lineTotal}"));
        }

        // Rule: All lines must be valid
        var invalidLines = batchLines.Where(l => !l.IsValid).Count();
        if (invalidLines > 0)
        {
            results.Add(new ReconciliationResult(
                Guid.NewGuid(),
                batchId,
                "INVALID_LINES",
                "High",
                0,
                invalidLines,
                $"{invalidLines} lines have validation errors"));
        }

        return results;
    }
}
