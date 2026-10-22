page 50000 "Pension Scheme List"
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Lists;
    SourceTable = "Pension Scheme";
    Caption = 'Pension Schemes';

    layout
    {
        area(content)
        {
            repeater(General)
            {
                field("Scheme Code"; Rec."Scheme Code")
                {
                    ApplicationArea = All;
                }
                field("Scheme Name"; Rec."Scheme Name")
                {
                    ApplicationArea = All;
                }
                field("BC Company Name"; Rec."BC Company Name")
                {
                    ApplicationArea = All;
                }
                field(Currency; Rec.Currency)
                {
                    ApplicationArea = All;
                }
                field(Status; Rec.Status)
                {
                    ApplicationArea = All;
                }
            }
        }
    }
}
