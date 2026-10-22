namespace PensionBridge.Infrastructure.BusinessCentral;

public interface IBusinessCentralClient
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
    Task<string> PostContributionAsync(string payload, CancellationToken cancellationToken = default);
}
