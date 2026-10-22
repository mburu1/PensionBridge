using PensionBridge.Domain.Reconciliation;

namespace PensionBridge.Application.Reconciliation;

public interface IReconciliationService
{
    Task<IEnumerable<ReconciliationResult>> ReconcileBatchAsync(Guid batchId, CancellationToken cancellationToken = default);
}
