namespace PensionBridge.Domain.Batches;

public enum BatchStatus
{
    Received,
    Validating,
    Rejected,
    ReconciliationPending,
    ExceptionReview,
    ReadyForApproval,
    Approved,
    QueuedForPosting,
    Posting,
    Posted,
    PostingUncertain,
    RetryScheduled,
    FailedPermanent
}
