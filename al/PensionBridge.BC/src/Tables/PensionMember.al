table 50001 "Pension Member"
{
    Caption = 'Pension Member';
    DataClassification = CustomerContent;

    fields
    {
        field(1; "Scheme Code"; Code[20])
        {
            Caption = 'Scheme Code';
            TableRelation = "Pension Scheme"."Scheme Code";
        }
        field(2; "Member Number"; Code[20])
        {
            Caption = 'Member Number';
        }
        field(3; "External Reference"; Text[50])
        {
            Caption = 'External Reference';
        }
        field(4; "Full Name"; Text[100])
        {
            Caption = 'Full Name';
        }
        field(5; "Join Date"; Date)
        {
            Caption = 'Join Date';
        }
        field(6; "Exit Date"; Date)
        {
            Caption = 'Exit Date';
        }
        field(7; Status; Enum "Scheme Status")
        {
            Caption = 'Status';
        }
        field(8; "Created Date"; DateTime)
        {
            Caption = 'Created Date';
            Editable = false;
        }
        field(9; "Modified Date"; DateTime)
        {
            Caption = 'Modified Date';
            Editable = false;
        }
    }

    keys
    {
        key(PK; "Scheme Code", "Member Number")
        {
            Clustered = true;
        }
        key(ExternalRef; "External Reference")
        {
        }
    }

    trigger OnInsert()
    begin
        "Created Date" := CurrentDateTime;
        "Modified Date" := CurrentDateTime;
    end;

    trigger OnModify()
    begin
        "Modified Date" := CurrentDateTime;
    end;
}
