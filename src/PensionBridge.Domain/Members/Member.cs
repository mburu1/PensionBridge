using PensionBridge.Domain.Schemes;

namespace PensionBridge.Domain.Members;

public class Member
{
    public Guid Id { get; private set; }
    public Guid SchemeId { get; private set; }
    public string MemberNumber { get; private set; } = string.Empty;
    public string? ExternalReference { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public DateTime JoinDate { get; private set; }
    public DateTime? ExitDate { get; private set; }
    public SchemeStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    private Member() { }

    public Member(Guid id, Guid schemeId, string memberNumber, string externalReference, string fullName, DateTime joinDate)
    {
        if (schemeId == Guid.Empty)
            throw new ArgumentException("Scheme ID is required", nameof(schemeId));
        
        if (string.IsNullOrWhiteSpace(memberNumber))
            throw new ArgumentException("Member number is required", nameof(memberNumber));
        
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required", nameof(fullName));

        Id = id;
        SchemeId = schemeId;
        MemberNumber = memberNumber;
        ExternalReference = externalReference;
        FullName = fullName;
        JoinDate = joinDate;
        Status = SchemeStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public void Exit(DateTime exitDate)
    {
        if (exitDate < JoinDate)
            throw new ArgumentException("Exit date cannot be before join date", nameof(exitDate));
        
        ExitDate = exitDate;
        Status = SchemeStatus.Inactive;
        ModifiedAt = DateTime.UtcNow;
    }

    public bool IsActiveOnDate(DateTime date)
    {
        return Status == SchemeStatus.Active && date >= JoinDate && (!ExitDate.HasValue || date <= ExitDate.Value);
    }
}
