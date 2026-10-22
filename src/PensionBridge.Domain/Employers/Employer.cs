namespace PensionBridge.Domain.Employers;

public class Employer
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string TaxId { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    private Employer() { }

    public Employer(Guid id, string code, string name, string taxId)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Employer code is required", nameof(code));
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Employer name is required", nameof(name));
        
        if (string.IsNullOrWhiteSpace(taxId))
            throw new ArgumentException("Tax ID is required", nameof(taxId));

        Id = id;
        Code = code;
        Name = name;
        TaxId = taxId;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Employer name is required", nameof(name));
        
        Name = name;
        ModifiedAt = DateTime.UtcNow;
    }
}
