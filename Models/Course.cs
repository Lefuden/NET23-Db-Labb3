using System;
using System.Collections.Generic;

namespace Labb3.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public bool? CourseActive { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
