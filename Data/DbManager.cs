using Labb3.Models;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;

namespace Labb3.Data;

internal class DbManager
{
    public Net23schoolContext Context { get; set; } = new();

    public List<string> GetAllClasses()
    {
        var classes = Context.Classes
            .Select(c => c.ClassName)
            .ToList();

        return classes;
    }

    public void GetClassInfo(string className)
    {
        var classInfo = Context.Students
            .Include(c => c.FkClass)
            .Where(c => c.FkClass.ClassName == className)
            .ToList();

        AnsiConsole.WriteLine($"Students of class {className}\n");
        foreach (var c in classInfo)
        {
            AnsiConsole.WriteLine($"{c.FirstName} {c.LastName}");
        }

        AnsiConsole.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
    }

    public void GetGrades()
    {
        var courses = Context.Courses
            .Include(c => c.Grades)
            .ToList();

        var grades = courses
            .Select(c => new
            {
                Course = c.CourseName,
                TopGrade = c.Grades.Max(t => t.Grade1),
                LowGrade = c.Grades.Min(t => t.Grade1),
                AverageGrade = c.Grades.Average(t => GetAverageGrade(t.Grade1))
            }).ToList();
            
        AnsiConsole.WriteLine("Course summary:\n");
        foreach (var g in grades)
        {
            AnsiConsole.WriteLine($"Course: {g.Course} \nGrades: Top [{g.TopGrade}] Low [{g.LowGrade}] Avg [{g.AverageGrade}]\n");
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
            //need to improve to also add recently added employees without a role if time
            roles = Context.EmployeeRoles
                .Include(t => t.FkEmployee)
                .Include(t => t.FkRole)
                .OrderBy(t => t.FkRole.Title)
                .ThenBy(t => t.FkEmployee.LastName)
                .ToList();
        }

        foreach (var r in roles)
        {
            AnsiConsole.WriteLine($"Name: {r.FkEmployee.FirstName} {r.FkEmployee.LastName}\nRole: {r.FkRole.Title}\n");
        }
        AnsiConsole.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
    }

    public void GetAllStudentsSorted(MenuChoice choice)
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
                Student student = AccountCreation.CreateStudent();
                Context.Students.Add(student);
                Context.SaveChanges();
                break;
            }
            case MenuChoice.CreateEmployee:
            {
                Employee employee = AccountCreation.CreateEmployee();
                Context.Employees.Add(employee);
                Context.SaveChanges();
                break;
            }
        }
    }
}