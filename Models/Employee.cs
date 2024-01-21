using System;
using System.Collections.Generic;

namespace Labb3.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? SocialSecurityNumber { get; set; }

    public DateOnly? EmploymentDate { get; set; }

    public string? Salary { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
