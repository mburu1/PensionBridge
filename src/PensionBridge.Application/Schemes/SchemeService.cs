using PensionBridge.Application.Abstractions;
using PensionBridge.Domain.Schemes;

namespace PensionBridge.Application.Schemes;

public class SchemeService : ISchemeService
{
    private readonly IRepository<Scheme> _repository;

    public SchemeService(IRepository<Scheme> repository)
    {
        _repository = repository;
    }

    public async Task<Scheme?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var schemes = await _repository.GetAllAsync(cancellationToken);
        return schemes.FirstOrDefault(s => s.Code == code);
    }

    public async Task<IEnumerable<Scheme>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<Scheme> CreateAsync(string code, string name, string businessCentralCompany, string currency, CancellationToken cancellationToken = default)
    {
        var scheme = new Scheme(Guid.NewGuid(), code, name, businessCentralCompany, currency);
        await _repository.AddAsync(scheme, cancellationToken);
        return scheme;
    }

    public async Task UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var scheme = await _repository.GetByIdAsync(id, cancellationToken);
        if (scheme == null)
            throw new InvalidOperationException($"Scheme with ID {id} not found");
        
        scheme.UpdateName(name);
        await _repository.UpdateAsync(scheme, cancellationToken);
    }
}
