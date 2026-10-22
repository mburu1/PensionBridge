namespace PensionBridge.Domain.Exceptions;

public class ExceptionCase
{
    public Guid Id { get; private set; }
    public Guid BatchId { get; private set; }
    public string ExceptionType { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string? AssignedTo { get; private set; }
    public string? Comments { get; private set; }
    public ApprovalDecision Decision { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    private ExceptionCase() { }

    public ExceptionCase(Guid id, Guid batchId, string exceptionType, string description)
    {
        Id = id;
        BatchId = batchId;
        ExceptionType = exceptionType;
        Description = description;
        Decision = ApprovalDecision.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void AssignTo(string userId)
    {
        AssignedTo = userId;
    }

    public void AddComment(string comment)
    {
        Comments = comment;
    }

    public void Approve(string approvedBy)
    {
        Decision = ApprovalDecision.Approved;
        ResolvedAt = DateTime.UtcNow;
    }

    public void Reject(string rejectedBy, string reason)
    {
        Decision = ApprovalDecision.Rejected;
        ResolvedAt = DateTime.UtcNow;
    }
}

public enum ApprovalDecision
{
    Pending,
    Approved,
    Rejected
}
