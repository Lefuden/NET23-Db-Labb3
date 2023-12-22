namespace Labb3.Models;

public partial class EmployeeRole
{
    public int? FkEmployeeId { get; set; }

    public int? FkRoleId { get; set; }

    public virtual Employee? FkEmployee { get; set; }

    public virtual Role? FkRole { get; set; }
}
