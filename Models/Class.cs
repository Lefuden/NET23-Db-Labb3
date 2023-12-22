namespace Labb3.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int? FkEmployeeId { get; set; }

    public virtual Employee? FkEmployee { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
