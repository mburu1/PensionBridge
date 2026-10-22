namespace PensionBridge.Domain.Audit;

public class AuditRecord
{
    public Guid Id { get; private set; }
    public string Actor { get; private set; } = string.Empty;
    public string Action { get; private set; } = string.Empty;
    public string EntityType { get; private set; } = string.Empty;
    public Guid EntityId { get; private set; }
    public string? Changes { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string? CorrelationId { get; private set; }

    private AuditRecord() { }

    public AuditRecord(Guid id, string actor, string action, string entityType, Guid entityId, string? changes, string? correlationId)
    {
        if (string.IsNullOrWhiteSpace(actor))
            throw new ArgumentException("Actor is required", nameof(actor));
        
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action is required", nameof(action));
        
        if (string.IsNullOrWhiteSpace(entityType))
            throw new ArgumentException("Entity type is required", nameof(entityType));

        Id = id;
        Actor = actor;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        Changes = changes;
        CorrelationId = correlationId;
        Timestamp = DateTime.UtcNow;
    }
}
