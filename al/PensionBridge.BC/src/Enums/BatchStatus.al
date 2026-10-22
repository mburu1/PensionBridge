enum 50001 "Batch Status"
{
    Extensible = true;
    AssignmentCompatibility = true;

    value(0; "Received")
    {
        Caption = 'Received';
    }
    value(1; "Validating")
    {
        Caption = 'Validating';
    }
    value(2; "Rejected")
    {
        Caption = 'Rejected';
    }
    value(3; "ReconciliationPending")
    {
        Caption = 'Reconciliation Pending';
    }
    value(4; "ExceptionReview")
    {
        Caption = 'Exception Review';
    }
    value(5; "ReadyForApproval")
    {
        Caption = 'Ready for Approval';
    }
    value(6; "Approved")
    {
        Caption = 'Approved';
    }
    value(7; "QueuedForPosting")
    {
        Caption = 'Queued for Posting';
    }
    value(8; "Posting")
    {
        Caption = 'Posting';
    }
    value(9; "Posted")
    {
        Caption = 'Posted';
    }
    value(10; "PostingUncertain")
    {
        Caption = 'Posting Uncertain';
    }
    value(11; "RetryScheduled")
    {
        Caption = 'Retry Scheduled';
    }
    value(12; "FailedPermanent")
    {
        Caption = 'Failed Permanent';
    }
}
