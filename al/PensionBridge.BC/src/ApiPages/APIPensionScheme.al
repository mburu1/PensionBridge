page 50010 "API - Pension Scheme"
{
    Caption = 'Pension Scheme';
    PageType = API;
    SourceTable = "Pension Scheme";
    EntityName = 'pensionScheme';
    EntitySetName = 'pensionSchemes';
    APIPublisher = 'pensionBridge';
    APIGroup = 'v1.0';
    APIVersion = 'v1.0';
    DelayedInsert = true;
    ODataKeyFields = "Scheme Code";

    layout
    {
        area(content)
        {
            repeater(GroupName)
            {
                field(schemeCode; Rec."Scheme Code")
                {
                    Caption = 'Scheme Code';
                }
                field(schemeName; Rec."Scheme Name")
                {
                    Caption = 'Scheme Name';
                }
                field(bcCompanyName; Rec."BC Company Name")
                {
                    Caption = 'BC Company Name';
                }
                field(currency; Rec.Currency)
                {
                    Caption = 'Currency';
                }
                field(status; Rec.Status)
                {
                    Caption = 'Status';
                }
                field(createdDate; Rec."Created Date")
                {
                    Caption = 'Created Date';
                }
                field(modifiedDate; Rec."Modified Date")
                {
                    Caption = 'Modified Date';
                }
            }
        }
    }
}
