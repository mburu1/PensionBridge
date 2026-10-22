namespace PensionBridge.Domain.Reconciliation;

public class ReconciliationResult
{
    public Guid Id { get; private set; }
    public Guid BatchId { get; private set; }
    public string Rule { get; private set; } = string.Empty;
    public string Severity { get; private set; } = string.Empty;
    public decimal ExpectedValue { get; private set; }
    public decimal ActualValue { get; private set; }
    public string? Evidence { get; private set; }
    public ResolutionState State { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ReconciliationResult() { }

    public ReconciliationResult(Guid id, Guid batchId, string rule, string severity, decimal expected, decimal actual, string? evidence)
    {
        Id = id;
        BatchId = batchId;
        Rule = rule;
        Severity = severity;
        ExpectedValue = expected;
        ActualValue = actual;
        Evidence = evidence;
        State = ResolutionState.Open;
        CreatedAt = DateTime.UtcNow;
    }

    public void Resolve(string resolution)
    {
        State = ResolutionState.Resolved;
    }

    public void MarkAsBlocked()
    {
        State = ResolutionState.Blocked;
    }
}

public enum ResolutionState
{
    Open,
    Resolved,
    Blocked
}
