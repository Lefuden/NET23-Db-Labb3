using Labb3.Models;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text;
using System.Linq;

namespace Labb3.Data;

internal class DbManager
{
    public Net23schoolContext Context { get; set; } = new();

    //private AccountCreation AccCr = new AccountCreation();
    //add instance of AccountCreation, dbm.
    //clean up static methods to non static.

    public List<string> GetAllClasses()
    {
        var classes = Context.Classes
            .Select(c => c.ClassName)
            .ToList();

        return classes;
    }

    public int GetClassId()
    {
        var className = UserInterface.GenericMenu(GetAllClasses(), "Select class:");
        var classId = Context.Classes
            .Where(c => c.ClassName == className)
            .Select(c => c.ClassId)
            .ToList();
        
        var cleanId = int.Parse(classId.First().ToString());
        
        AnsiConsole.WriteLine($"Getting the classID of {className}: {cleanId}");
        Console.ReadKey();
        return cleanId;
    }

    public List<Course> GetAllCourses()
    {
        var courses = Context.Courses
            .ToList();
        return courses;
    }

    public List<string> GetAllEmployeeRoles()
    {
        var roles = Context.Roles
            .Select(c => c.Title)
            .ToList();

        return roles;
    }

    public int GetRoleId() //testing different approach compared to GetClassId();
    {
        var roleTitle = UserInterface.GenericMenu(GetAllEmployeeRoles(), "Select role:");
        int roleId;
        switch (roleTitle)
        {
            case "Teacher":
                roleId = 1;
                break;
            case "Principal":
                roleId = 2;
                break;
            case "Janitor":
                roleId = 3;
                break;
            case "Administrator":
                roleId = 4;
                break;
            default:
                roleId = 5;
                break;
        }

        Console.WriteLine($"getting role id for {roleTitle}: {roleId}");
        Console.ReadKey();
        return roleId;
    }

    public void GetClassInfo()
    {
        var className = UserInterface.GenericMenu(GetAllClasses(), "Select class:");
        var classInfo = Context.Students
            .Include(c => c.FkClass)
            .Where(c => c.FkClass != null && c.FkClass.ClassName == className)
            .ToList();

        AnsiConsole.WriteLine($"Students of class {className}\n");
        foreach (var c in classInfo)
        {
            AnsiConsole.WriteLine($"{c.FirstName} {c.LastName}");
        }

        AnsiConsole.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
    }

    public void AddGrade() //this is a terrible, terrible method, but it works.
    {
        var courses = GetAllCourses();
        var courseChoice = UserInterface.GenericMenu(courses.Select(c => c.CourseName).ToList(), "Select course:");

        var students = Context.StudentCourses
            .Include(c => c.FkCourse)
            .Include(c => c.FkStudent)
            .Where(c => c.FkCourse != null && c.FkCourse.CourseName == courseChoice)
            .ToList();

        var queryDictStudent = Context.StudentCourses
            .Include(s => s.FkCourse)
            .Include(s => s.FkStudent)
            .Where(c => c.FkCourse != null && c.FkCourse.CourseName == courseChoice);

        var studentChoice = UserInterface.GenericMenu(queryDictStudent.Select(c => c.FkStudent.FirstName + " " + c.FkStudent.LastName).ToList(), "Select student:");

        var teachers = Context.EmployeeCourses
            .Include(c => c.FkEmployee)
            .Include(c => c.FkCourse)
            .Where(c => c.FkCourse != null && c.FkCourse.CourseName == courseChoice)
            .ToList();

        var teacherChoice = UserInterface.GenericMenu(teachers.Select(c => c.FkEmployee.FirstName + " " + c.FkEmployee.LastName).ToList(), "Select teacher:");

        var grades = new List<string> { "1", "2", "3", "4", "5" }; //could make an enum
        var gradeChoice = UserInterface.GenericMenu(grades, "Select grade:");

        var transaction = Context.Database.BeginTransaction();
        try
        {
            var course = courses.FirstOrDefault(c => c.CourseName == courseChoice);
            var student = students.FirstOrDefault(c => c.FkStudent.FirstName + " " + c.FkStudent.LastName == studentChoice);
            var employee = teachers.FirstOrDefault(c => c.FkEmployee.FirstName + " " + c.FkEmployee.LastName == teacherChoice);

            Grade newGrade = new()
            {
                Grade1 = gradeChoice,
                GradeDate = DateOnly.FromDateTime(DateTime.Now),
                FkCourseId = course.CourseId,
                FkStudentId = student.FkStudent.StudentId,
                FkEmployeeId = employee.FkEmployee.EmployeeId
            };
            Context.Grades.Add(newGrade);
            Context.SaveChanges();
            transaction.Commit();
            AnsiConsole.WriteLine("A new grade has been registered");
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }

    public void GetGrades()
    {
        var menuSelection = UserInterface.CourseMenu();
        var courses = Context.Courses
            .Include(c => c.Grades)
            .ToList();

        var grades = courses
            .Select(c => new
            {
                Course = c.CourseName,
                TopGrade = c.Grades.Max(t => t.Grade1),
                LowGrade = c.Grades.Min(t => t.Grade1),
                AverageGrade = c.Grades.Average(t => GetAverageGrade(t.Grade1)),
                IsActive = c.CourseActive
            }).ToList();

        switch (menuSelection)
        {
            case "All":
                AnsiConsole.WriteLine("Course summary:\n");
                foreach (var g in grades)
                {
                    AnsiConsole.WriteLine($"Course: {g.Course} \nGrades: Top [{g.TopGrade}] Low [{g.LowGrade}] Avg [{g.AverageGrade}]\n");
                }
                break;
            case "Active":
                AnsiConsole.WriteLine("Active courses:\n");
                foreach (var g in grades.Where(g => g.IsActive == true))
                {
                    AnsiConsole.WriteLine($"Course: {g.Course} \nGrades: Top [{g.TopGrade}] Low [{g.LowGrade}] Avg [{g.AverageGrade}]\n");
                }
                break;
            case "Inactive":
                AnsiConsole.WriteLine("Inactive courses:\n");
                foreach (var g in grades.Where(g => g.IsActive == false))
                {
                    AnsiConsole.WriteLine($"Course: {g.Course} \nGrades: Top [{g.TopGrade}] Low [{g.LowGrade}] Avg [{g.AverageGrade}]\n");
                }
                break;
        }

        AnsiConsole.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
    }

    public int GetAverageGrade(string grade) => Convert.ToInt32(grade);

    public void GetRecentGrades()
    {
        var lastMonth = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
        var recentGrades = Context.Grades
            .Include(r => r.FkStudent)
            .Include(r => r.FkCourse)
            .Include(r => r.FkEmployee)
            .Where(r => r.GradeDate > lastMonth)
            .ToList();

        AnsiConsole.WriteLine($"List of grades registered in the last 30 days [{lastMonth}]\n");
        foreach (var r in recentGrades)
        {
            AnsiConsole.WriteLine($"Student: {r.FkStudent.FirstName} {r.FkStudent.LastName} \nCourse: {r.FkCourse.CourseName} \nGrade: {r.Grade1} \nTeacher: {r.FkEmployee.FirstName} {r.FkEmployee.LastName}\n");
        }
        AnsiConsole.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
    }

    public void GetAllEmployeesByRole(string employeeRole)
    {
        //var roleTitle = UserInterface.RoleMenu(GetAllEmployeeRoles()); //can add this
        var roles = new List<EmployeeRole>();

        if (employeeRole != "All")
        {
            roles = Context.EmployeeRoles
                .Include(t => t.FkEmployee)
                .Include(t => t.FkRole)
                .Where(t => t.FkRole.Title == employeeRole)
                .OrderBy(t => t.FkRole.Title)
                .ThenBy(t => t.FkEmployee.LastName)
                .ToList();
        }
        else
        {
            roles = Context.EmployeeRoles
                .Include(t => t.FkEmployee)
                .Include(t => t.FkRole)
                .OrderBy(t => t.FkRole.Title)
                .ThenBy(t => t.FkEmployee.LastName)
                .ToList();
        }

        foreach (var r in roles)
        {
            AnsiConsole.WriteLine($"Name: {r.FkEmployee.FirstName} {r.FkEmployee.LastName}\nRole: {r.FkRole.Title}" +
                                  $"\nYears of service: {GetYearsOfService(r.FkEmployee.EmploymentDate.Value)}\nSalary: {r.FkEmployee.Salary}\n");
        }
        AnsiConsole.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
    }

    public string GetYearsOfService(DateOnly employee)
    {
        var employmentDate = employee.ToDateTime(TimeOnly.FromDateTime(DateTime.Now));
        var daysOfService = (DateTime.Now - employmentDate).TotalDays;
        var timeBuilder = new StringBuilder();

        if (daysOfService >= 365)
        {
            var years = daysOfService / 365;
            timeBuilder.Append($"{years:.} year{(years > 1 ? "s" : "")} ");
        }
        else
        {
            timeBuilder.Append($"Less than one year");
        }

        return timeBuilder.ToString();
    }

    public void GetSalaryPerRole()
    {
        var roleTitle = UserInterface.GenericMenu(GetAllEmployeeRoles(), "Select role:");
        var roles = Context.EmployeeRoles
            .Include(t => t.FkEmployee)
            .Include(t => t.FkRole)
            .Where(t => t.FkRole.Title == roleTitle)
            .ToList();
        
        var totalSalary = roles
            .Where(r => r.FkEmployee.Salary != null)
            .Sum(r => int.Parse(r.FkEmployee.Salary));

        var roleAvg = roles
            .Select(e => decimal.TryParse(e.FkEmployee.Salary, out decimal salary) ? salary : 0)
            .Average();

        AnsiConsole.WriteLine($"Total monthly salary paid to {roleTitle}s: {totalSalary}\n" +
                              $"Average monthly salary: {roleAvg:0}");

        GetTotalSalary();

        AnsiConsole.WriteLine($"\n\nPress any key to continue");
        Console.ReadKey();
    }

    public void GetTotalSalary() //add menu to pick between roles or all?
    {
        var totalAvg = Context.Employees
            .AsEnumerable()
            .Select(e => decimal.TryParse(e.Salary, out decimal salary) ? salary : 0)
            .Average();

        AnsiConsole.WriteLine($"Total average salary paid to all roles: {totalAvg}");
    }

    public void GetAllStudentsSorted(MenuChoice choice) //optimize this
    {
        var listStudents = Context.Students
            .Include(s => s.FkClass)
            .ToList();
        List<Student> sortStudents;
        
        switch (choice)
        {
            case MenuChoice.FirstNameAsc:
                sortStudents = listStudents.OrderBy(s => s.FirstName).ToList();

                foreach (var s in sortStudents)
                {
                    AnsiConsole.Write($"Name: {s.FirstName} {s.LastName}\nSocial security number: {s.SocialSecurityNr}\n");
                    AnsiConsole.Write(s.FkClass != null ? $"Class: {s.FkClass.ClassName}\n\n" : "Class: Unassigned\n\n");
                }
                break;

            case MenuChoice.FirstNameDesc:
                sortStudents = listStudents.OrderByDescending(s => s.FirstName).ToList();

                foreach (var s in sortStudents)
                {
                    AnsiConsole.Write($"Name: {s.FirstName} {s.LastName}\nSocial security number: {s.SocialSecurityNr}\n");
                    AnsiConsole.Write(s.FkClass != null ? $"Class: {s.FkClass.ClassName}\n\n" : "Class: Unassigned\n\n");
                }
                break;

            case MenuChoice.LastNameAsc:
                sortStudents = listStudents.OrderBy(s => s.LastName).ToList();
                
                foreach (var s in sortStudents)
                {
                    AnsiConsole.Write($"Name: {s.LastName} {s.FirstName}\nSocial security number: {s.SocialSecurityNr}\n");
                    AnsiConsole.Write(s.FkClass != null ? $"Class: {s.FkClass.ClassName}\n\n" : "Class: Unassigned\n\n");
                }
                break;

            case MenuChoice.LastNameDesc:
                sortStudents = listStudents.OrderByDescending(s => s.LastName).ToList();

                foreach (var s in sortStudents)
                {
                    AnsiConsole.Write($"Name: {s.LastName} {s.FirstName}\nSocial security number: {s.SocialSecurityNr}\n");
                    AnsiConsole.Write(s.FkClass != null ? $"Class: {s.FkClass.ClassName}\n\n" : "Class: Unassigned\n\n");
                }
                break;
        }
        AnsiConsole.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
    }

    public void AddPerson(MenuChoice type)
    {
        switch (type)
        {
            case MenuChoice.CreateStudent:
            {
                var transaction = Context.Database.BeginTransaction();
                try
                {
                    Student student = AccountCreation.CreateStudent();
                    Context.Students.Add(student);
                    Context.SaveChanges();
                    transaction.Commit();
                    AnsiConsole.WriteLine("A new student has been registered");
                    AnsiConsole.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
                    break;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e);
                    throw;
                }
            }
            case MenuChoice.CreateEmployee: 
            {
                var transaction = Context.Database.BeginTransaction();
                try
                {
                    Employee employee = AccountCreation.CreateEmployee();
                    Context.Employees.Add(employee);
                    Context.SaveChanges();

                    var roleId = GetRoleId();

                    EmployeeRole role = new()
                    {
                        FkEmployeeId = employee.EmployeeId,
                        FkRoleId = roleId,
                    };

                    Context.EmployeeRoles.Add(role);
                    Context.SaveChanges();
                    transaction.Commit();
                    AnsiConsole.WriteLine("A new employee has been registered");
                    AnsiConsole.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
                    break;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e);
                    throw;
                }


            }
        }
    }

}