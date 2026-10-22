namespace PensionBridge.Domain.Schemes;

public class Scheme
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string BusinessCentralCompany { get; private set; } = string.Empty;
    public string Currency { get; private set; } = string.Empty;
    public SchemeStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    private Scheme() { }

    public Scheme(Guid id, string code, string name, string businessCentralCompany, string currency)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Scheme code is required", nameof(code));
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Scheme name is required", nameof(name));
        
        if (string.IsNullOrWhiteSpace(businessCentralCompany))
            throw new ArgumentException("Business Central company is required", nameof(businessCentralCompany));
        
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required", nameof(currency));

        Id = id;
        Code = code;
        Name = name;
        BusinessCentralCompany = businessCentralCompany;
        Currency = currency.ToUpperInvariant();
        Status = SchemeStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Scheme name is required", nameof(name));
        
        Name = name;
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetStatus(SchemeStatus status)
    {
        Status = status;
        ModifiedAt = DateTime.UtcNow;
    }

    public bool IsActive() => Status == SchemeStatus.Active;
}
