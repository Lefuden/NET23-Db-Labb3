using Labb3.Models;
using Spectre.Console;

namespace Labb3.Data;

//need to expand to be able to add more info. employee role, student class etc.
//something weird is happening with the IDs as well, but it's working.
internal class AccountCreation 
{
    public static Student CreateStudent()
    {
        var (firstName, lastName, socialSecurityNr) = GetInformation();

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

    public static Employee CreateEmployee()
    {
        var (firstName, lastName, socialSecurityNr) = GetInformation();

        return new Employee
        {
            //EmployeeId = 0,
            FirstName = firstName,
            LastName = lastName,
            SocialSecurityNumber = socialSecurityNr,
            //Classes = null,
            //Grades = null
        };
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
                    Console.Clear();
                    return (firstName, lastName, socialSecurityNr);
                case false:
                    Console.Clear();
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
}