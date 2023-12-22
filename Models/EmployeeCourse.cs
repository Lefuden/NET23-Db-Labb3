namespace Labb3.Models;

public partial class EmployeeCourse
{
    public int? FkEmployeeId { get; set; }

    public int? FkCourseId { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Employee? FkEmployee { get; set; }
}
