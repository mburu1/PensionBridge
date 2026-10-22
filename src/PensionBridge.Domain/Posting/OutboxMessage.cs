namespace PensionBridge.Domain.Posting;

public class OutboxMessage
{
    public Guid Id { get; private set; }
    public string MessageType { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;
    public string IdempotencyKey { get; private set; } = string.Empty;
    public OutboxStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public int RetryCount { get; private set; }

    private OutboxMessage() { }

    public OutboxMessage(Guid id, string messageType, string payload, string idempotencyKey)
    {
        Id = id;
        MessageType = messageType;
        Payload = payload;
        IdempotencyKey = idempotencyKey;
        Status = OutboxStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        RetryCount = 0;
    }

    public void MarkAsProcessing()
    {
        Status = OutboxStatus.Processing;
    }

    public void MarkAsProcessed()
    {
        Status = OutboxStatus.Processed;
        ProcessedAt = DateTime.UtcNow;
    }

    public void IncrementRetry()
    {
        RetryCount++;
    }

    public void MarkAsFailed()
    {
        Status = OutboxStatus.Failed;
    }
}

public enum OutboxStatus
{
    Pending,
    Processing,
    Processed,
    Failed
}
