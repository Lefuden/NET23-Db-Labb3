namespace Labb3.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? SocialSecurityNr { get; set; }

    public int? FkClassId { get; set; }

    public virtual Class? FkClass { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
