using Labb3.Models;
using Spectre.Console;

namespace Labb3.Data;

internal class AccountCreation
{
    private static DbManager dbManager = new();
    public static Student CreateStudent()
    {
        var (firstName, lastName, socialSecurityNr) = GetInformation();
        var classId = dbManager.GetClassId();

        return new Student
        {
            FirstName = firstName,
            LastName = lastName,
            SocialSecurityNr = socialSecurityNr,
            FkClassId = classId,
        };
    }

    public static Employee CreateEmployee() 
    {
        while (true)
        {
            var (firstName, lastName, socialSecurityNr) = GetInformation();
            var employmentDate = GetValidDate();
            var salary = GetSalary();

            AnsiConsole.Clear();
            AnsiConsole.WriteLine($"Full Name: {firstName} {lastName}\nSocial security number: {socialSecurityNr}\n" +
                                  $"Employment date: {employmentDate}\nSalary: {salary}");

            switch (UserInterface.AskYesOrNo("Is this information correct?"))
            {
                case true:
                    AnsiConsole.Clear();
                    return new Employee
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        SocialSecurityNumber = socialSecurityNr,
                        EmploymentDate = employmentDate,
                        Salary = salary
                    };
                case false:
                    AnsiConsole.Clear();
                    break;
            }
        }
    }

    public static (string FirstName, string LastName, string socialSecurityNr) GetInformation()
    {
        while (true)
        {
            AnsiConsole.WriteLine("Enter information");
            var firstName = AnsiConsole.Ask<string>("First name:");
            var lastName = AnsiConsole.Ask<string>("Last name:");
            var socialSecurityNr = GetSocialSecurityNr();

            AnsiConsole.Clear();
            AnsiConsole.WriteLine($"Full Name: {firstName} {lastName}\nSocial security number: {socialSecurityNr}");

            switch (UserInterface.AskYesOrNo("Is this information correct?"))
            {
                case true:
                    AnsiConsole.Clear();
                    return (firstName, lastName, socialSecurityNr);
                case false:
                    AnsiConsole.Clear();
                    break;
            }
        }
    }

    public static string GetSocialSecurityNr()
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>("Social security number (YYYYMMDDXXXX):");
            if (input.Length == 12 && ulong.TryParse(input, out var r))
            {
                return input;
            }
            AnsiConsole.WriteLine("Invalid format, try again");
        }
    }

    public static string GetSalary()
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>("Enter monthly salary amount:");
            if (input.Length <= 10 && ulong.TryParse(input, out var r))
            {
                return input;
            }
            AnsiConsole.WriteLine("Invalid format, try again");
        }
    }

    public static DateOnly GetValidDate()
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>("Employment date (YYYYMMDD):");
            if (input.Length == 8)
            {
                input = input.Insert(6, ",");
                input = input.Insert(4, ",");
            }

            if (DateOnly.TryParse(input, out var validDateFormat))
            {
                return validDateFormat;
            }
            AnsiConsole.WriteLine("Invalid format, try again");
        }
    }
}