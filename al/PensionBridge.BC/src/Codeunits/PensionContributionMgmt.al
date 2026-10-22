codeunit 50000 "Pension Contribution Mgmt"
{
    TableNo = "Pension Scheme";
    trigger OnRun()
    begin
        // Placeholder for contribution posting logic
    end;

    procedure ValidateScheme(SchemeCode: Code[20]): Boolean
    var
        PensionScheme: Record "Pension Scheme";
    begin
        if PensionScheme.Get(SchemeCode) then
            exit(PensionScheme.Status = PensionScheme.Status::Active);

        exit(false);
    end;

    procedure ValidateMember(SchemeCode: Code[20]; MemberNumber: Code[20]): Boolean
    var
        PensionMember: Record "Pension Member";
    begin
        if PensionMember.Get(SchemeCode, MemberNumber) then
            exit(PensionMember.Status = PensionMember.Status::Active);

        exit(false);
    end;
}
