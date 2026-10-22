using PensionBridge.Domain.Common;

namespace PensionBridge.Domain.Batches;

public class ContributionBatch
{
    public Guid Id { get; private set; }
    public Guid EmployerId { get; private set; }
    public Guid SchemeId { get; private set; }
    public string Period { get; private set; } = string.Empty;
    public string FileName { get; private set; } = string.Empty;
    public string FileHash { get; private set; } = string.Empty;
    public string IdempotencyKey { get; private set; } = string.Empty;
    public BatchStatus Status { get; private set; }
    public int TotalRows { get; private set; }
    public int AcceptedRows { get; private set; }
    public int RejectedRows { get; private set; }
    public Money TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public string? RejectionReason { get; private set; }

    private ContributionBatch() { }

    public ContributionBatch(Guid id, Guid employerId, Guid schemeId, string period, string fileName, string fileHash, string idempotencyKey, Money totalAmount)
    {
        if (employerId == Guid.Empty)
            throw new ArgumentException("Employer ID is required", nameof(employerId));
        
        if (schemeId == Guid.Empty)
            throw new ArgumentException("Scheme ID is required", nameof(schemeId));
        
        if (string.IsNullOrWhiteSpace(period))
            throw new ArgumentException("Period is required", nameof(period));
        
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name is required", nameof(fileName));
        
        if (string.IsNullOrWhiteSpace(fileHash))
            throw new ArgumentException("File hash is required", nameof(fileHash));
        
        if (string.IsNullOrWhiteSpace(idempotencyKey))
            throw new ArgumentException("Idempotency key is required", nameof(idempotencyKey));

        Id = id;
        EmployerId = employerId;
        SchemeId = schemeId;
        Period = period;
        FileName = fileName;
        FileHash = fileHash;
        IdempotencyKey = idempotencyKey;
        Status = BatchStatus.Received;
        TotalAmount = totalAmount;
        CreatedAt = DateTime.UtcNow;
    }

    public void StartValidation()
    {
        if (Status != BatchStatus.Received)
            throw new InvalidOperationException("Batch must be in Received status to start validation");
        
        Status = BatchStatus.Validating;
        ModifiedAt = DateTime.UtcNow;
    }

    public void CompleteValidation(int totalRows, int acceptedRows, int rejectedRows)
    {
        if (Status != BatchStatus.Validating)
            throw new InvalidOperationException("Batch must be in Validating status to complete validation");
        
        TotalRows = totalRows;
        AcceptedRows = acceptedRows;
        RejectedRows = rejectedRows;
        
        if (rejectedRows > 0)
        {
            Status = BatchStatus.ReconciliationPending;
        }
        else
        {
            Status = BatchStatus.ReadyForApproval;
        }
        
        ModifiedAt = DateTime.UtcNow;
    }

    public void Reject(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Rejection reason is required", nameof(reason));
        
        Status = BatchStatus.Rejected;
        RejectionReason = reason;
        ModifiedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        if (Status != BatchStatus.ReadyForApproval)
            throw new InvalidOperationException("Batch must be in ReadyForApproval status to approve");
        
        Status = BatchStatus.Approved;
        ModifiedAt = DateTime.UtcNow;
    }

    public void QueueForPosting()
    {
        if (Status != BatchStatus.Approved)
            throw new InvalidOperationException("Batch must be Approved to queue for posting");
        
        Status = BatchStatus.QueuedForPosting;
        ModifiedAt = DateTime.UtcNow;
    }

    public void MarkAsPosted()
    {
        if (Status != BatchStatus.Posting && Status != BatchStatus.PostingUncertain)
            throw new InvalidOperationException("Batch must be in Posting or PostingUncertain status to mark as posted");
        
        Status = BatchStatus.Posted;
        ModifiedAt = DateTime.UtcNow;
    }

    public void MarkAsPostingUncertain()
    {
        if (Status != BatchStatus.Posting)
            throw new InvalidOperationException("Batch must be in Posting status to mark as uncertain");
        
        Status = BatchStatus.PostingUncertain;
        ModifiedAt = DateTime.UtcNow;
    }

    public void ScheduleRetry()
    {
        if (Status != BatchStatus.PostingUncertain)
            throw new InvalidOperationException("Batch must be in PostingUncertain status to schedule retry");
        
        Status = BatchStatus.RetryScheduled;
        ModifiedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = BatchStatus.FailedPermanent;
        ModifiedAt = DateTime.UtcNow;
    }
}
