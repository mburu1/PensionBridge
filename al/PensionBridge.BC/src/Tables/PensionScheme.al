table 50000 "Pension Scheme"
{
    Caption = 'Pension Scheme';
    DataClassification = CustomerContent;

    fields
    {
        field(1; "Scheme Code"; Code[20])
        {
            Caption = 'Scheme Code';
            NotBlank = true;
        }
        field(2; "Scheme Name"; Text[100])
        {
            Caption = 'Scheme Name';
        }
        field(3; "BC Company Name"; Text[100])
        {
            Caption = 'BC Company Name';
            TableRelation = Company.Name;
        }
        field(4; Currency; Code[10])
        {
            Caption = 'Currency';
            TableRelation = Currency.Code;
        }
        field(5; Status; Enum "Scheme Status")
        {
            Caption = 'Status';
        }
        field(6; "Created Date"; DateTime)
        {
            Caption = 'Created Date';
            Editable = false;
        }
        field(7; "Modified Date"; DateTime)
        {
            Caption = 'Modified Date';
            Editable = false;
        }
    }

    keys
    {
        key(PK; "Scheme Code")
        {
            Clustered = true;
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
