using System;
using System.Collections.Generic;

namespace Labb3.Models;

public partial class StudentCourse
{
    public int? FkStudentId { get; set; }

    public int? FkCourseId { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Student? FkStudent { get; set; }
}
