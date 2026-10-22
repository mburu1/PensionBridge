using PensionBridge.Domain.Batches;
using PensionBridge.Domain.Common;

namespace PensionBridge.Application.Ingestion;

public interface IContributionIngestionService
{
    Task<ContributionBatch> IngestCsvAsync(
        Guid employerId,
        Guid schemeId,
        string period,
        Stream csvStream,
        string fileName,
        CancellationToken cancellationToken = default);
}
