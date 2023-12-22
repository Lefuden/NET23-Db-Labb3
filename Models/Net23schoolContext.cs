using Microsoft.EntityFrameworkCore;

namespace Labb3.Models;

public partial class Net23schoolContext : DbContext
{
    public Net23schoolContext()
    {
    }

    public Net23schoolContext(DbContextOptions<Net23schoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCourse> EmployeeCourses { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NET23School;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A086545288");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(30);
            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Classes)
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK__Classes__FK_Empl__2B3F6F97");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D718705F05721");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(30);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11B4D55346");

            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(12);
        });

        modelBuilder.Entity<EmployeeCourse>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

            entity.HasOne(d => d.FkCourse).WithMany()
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK__EmployeeC__FK_Co__35BCFE0A");

            entity.HasOne(d => d.FkEmployee).WithMany()
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK__EmployeeC__FK_Em__34C8D9D1");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");
            entity.Property(e => e.FkRoleId).HasColumnName("FK_RoleID");

            entity.HasOne(d => d.FkEmployee).WithMany()
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK__EmployeeR__FK_Em__276EDEB3");

            entity.HasOne(d => d.FkRole).WithMany()
                .HasForeignKey(d => d.FkRoleId)
                .HasConstraintName("FK__EmployeeR__FK_Ro__286302EC");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A379860EE4E");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(5)
                .HasColumnName("Grade");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_Grades_Courses");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK_Grades_Employees");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_Grades_Students");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AD13AD9D0");

            entity.Property(e => e.Title).HasMaxLength(30);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A79EB7E76AF");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassID");
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.SocialSecurityNr).HasMaxLength(12);

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK__Students__FK_Cla__2E1BDC42");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

            entity.HasOne(d => d.FkCourse).WithMany()
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_StudentCourses_Courses");

            entity.HasOne(d => d.FkStudent).WithMany()
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_StudentCourses_Students");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
