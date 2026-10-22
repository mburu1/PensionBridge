using PensionBridge.Domain.Common;

namespace PensionBridge.Domain.Contributions;

public class ContributionLine
{
    public Guid Id { get; private set; }
    public Guid BatchId { get; private set; }
    public Guid MemberId { get; private set; }
    public int SourceRowNumber { get; private set; }
    public Money EmployerContribution { get; private set; }
    public Money EmployeeContribution { get; private set; }
    public Money VoluntaryContribution { get; private set; }
    public bool IsValid { get; private set; }
    public string? ValidationErrorMessage { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ContributionLine() { }

    public ContributionLine(Guid id, Guid batchId, Guid memberId, int sourceRowNumber, Money employerContribution, Money employeeContribution, Money voluntaryContribution)
    {
        if (batchId == Guid.Empty)
            throw new ArgumentException("Batch ID is required", nameof(batchId));
        
        if (memberId == Guid.Empty)
            throw new ArgumentException("Member ID is required", nameof(memberId));
        
        if (sourceRowNumber <= 0)
            throw new ArgumentException("Source row number must be positive", nameof(sourceRowNumber));

        Id = id;
        BatchId = batchId;
        MemberId = memberId;
        SourceRowNumber = sourceRowNumber;
        EmployerContribution = employerContribution;
        EmployeeContribution = employeeContribution;
        VoluntaryContribution = voluntaryContribution;
        IsValid = true;
        CreatedAt = DateTime.UtcNow;
    }

    public Money GetTotalContribution()
    {
        var total = EmployerContribution.Add(EmployeeContribution);
        return total.Add(VoluntaryContribution);
    }

    public void MarkAsInvalid(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentException("Error message is required", nameof(errorMessage));
        
        IsValid = false;
        ValidationErrorMessage = errorMessage;
    }
}
