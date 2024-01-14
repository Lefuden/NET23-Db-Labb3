using Labb3.Models;
using Spectre.Console;

namespace Labb3.Data;

//need to expand to be able to add more info. employee role, student class etc.
//something weird is happening with the IDs as well, but it's working.
internal class AccountCreation
{
    private static DbManager dbManager = new DbManager();
    public static Student CreateStudent() //add class
    {
        var (firstName, lastName, socialSecurityNr) = GetInformation();
        var className = UserInterface.ClassMenu(dbManager.GetAllClasses());
        //AccountCreation can't access DbM, might have to rethink this.

        return new Student
        {
            //StudentId = null,
            FirstName = firstName,
            LastName = lastName,
            SocialSecurityNr = socialSecurityNr,
            //FkClassId = null,
            //FkClass = null,
            //Grades = null
        };
    }

    public static Employee CreateEmployee() //add EmploymentDate, Salary
    {
        while (true)
        {
            var (firstName, lastName, socialSecurityNr) = GetInformation();
            
            //var roleTitle = UserInterface.RoleMenu(DbM.GetAllEmployeeRoles());
            //AccountCreation can't access DbM, might have to rethink this.

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
                        //EmployeeId = 0,
                        FirstName = firstName,
                        LastName = lastName,
                        SocialSecurityNumber = socialSecurityNr,
                        //Classes = null,
                        //Grades = null,
                        //EmploymentDate = employmentDate,
                        //Salary = salary,
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
            //parse only to check if input is clean, output is not used. expand to validate nr if i have enough time.
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

    public static DateTime GetValidDate()
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>("Employment date (YYYYMMDD):");
            if (input.Length == 8)
            {
                input = input.Insert(6, ",");
                input = input.Insert(4, ",");
            }

            if (DateTime.TryParse(input, out DateTime validDateFormat))
            {
                return validDateFormat;
            }

            AnsiConsole.WriteLine("Invalid format, try again");
        }
    }
}