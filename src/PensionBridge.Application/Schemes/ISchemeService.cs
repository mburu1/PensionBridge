using PensionBridge.Domain.Schemes;

namespace PensionBridge.Application.Schemes;

public interface ISchemeService
{
    Task<Scheme?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Scheme>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Scheme> CreateAsync(string code, string name, string businessCentralCompany, string currency, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default);
}
