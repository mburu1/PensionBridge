permissionset 50000 "Pension Bridge - Read"
{
    Access = Public;
    Assignable = true;
    Caption = 'Pension Bridge - Read';

    Permissions = tabledata "Pension Scheme" = R,
                  tabledata "Pension Member" = R;
}

permissionset 50001 "Pension Bridge - Edit"
{
    Access = Public;
    Assignable = true;
    Caption = 'Pension Bridge - Edit';

    IncludedPermissionSets = "Pension Bridge - Read";

    Permissions = tabledata "Pension Scheme" = IMD,
                  tabledata "Pension Member" = IMD;
}

permissionset 50002 "Pension Bridge - Admin"
{
    Access = Public;
    Assignable = true;
    Caption = 'Pension Bridge - Admin';

    IncludedPermissionSets = "Pension Bridge - Edit";

    Permissions = tabledata "Pension Scheme" = RIMD,
                  tabledata "Pension Member" = RIMD;
}
