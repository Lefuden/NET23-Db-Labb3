using Spectre.Console;

namespace Labb3.Data;

internal class UserInterface
{
    public static MenuChoice StartMenu()
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Main menu")
            .AddChoices(new[]
                {
                    "Students",
                    "Employees",
                    "Grades",
                    "Classes",
                    "Courses",
                    "Registration",
                    "Exit"
                }
            ));
        return ConvertToMenuChoice(choice);
    }

    public static MenuChoice ViewEmployeesMenu()
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select employees to view by role")
            .AddChoices(new[]
                {
                    "All employees",
                    "Teacher",
                    "Janitor",
                    "Administrator",
                    "Principal",
                    "Back"
                }
            ));
        return ConvertToMenuChoice(choice);
    }

    public static MenuChoice ViewStudentsMenu()
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("List students sorted by:")
            .AddChoices(new[]
                {
                    "First name, ascending",
                    "First name, descending",
                    "Last name, ascending",
                    "Last name, descending",
                    "Back"
                }
            ));
        return ConvertToMenuChoice(choice);
    }

    public static string ClassMenu(List<string> choiceList)
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select class:")
            .AddChoices(choiceList)
        );
        return choice;
    }

    public static string CourseMenu()
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select what courses to view:")
            .AddChoices(new []
            {
                "All", "Active", "Inactive",
            }));
        return choice;
    }

    public static string RoleMenu(List<string> choiceList) //work in progress, use with AccountCreation
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select role:")
            .AddChoices(choiceList)
        );
        return choice;
    }

    public static MenuChoice CreationMenu()
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("What would you like to add:")
            .AddChoices(new[]
                {
                    "New student",
                    "New employee",
                    "Back"
                }
            ));
        return ConvertToMenuChoice(choice);
    }

    public static MenuChoice ConvertToMenuChoice(string input)
    {
        return input switch
        {
            "Students" => MenuChoice.StudentMenu,
            "Employees" => MenuChoice.EmployeeMenu,
            "Grades" => MenuChoice.Grades,
            "Classes" => MenuChoice.Classes,
            "Courses" => MenuChoice.Courses,
            "Teacher" => MenuChoice.Teacher,
            "Principal" => MenuChoice.Principal,
            "Administrator" => MenuChoice.Administrator,
            "Janitor" => MenuChoice.Janitor,
            "New student" => MenuChoice.CreateStudent,
            "New employee" => MenuChoice.CreateEmployee,
            "Back" => MenuChoice.Back,
            "Exit" => MenuChoice.Exit,
            "All employees" => MenuChoice.All,
            "Registration" => MenuChoice.Registration,
            "First name, ascending" => MenuChoice.FirstNameAsc,
            "First name, descending" => MenuChoice.FirstNameDesc,
            "Last name, ascending" => MenuChoice.LastNameAsc,
            "Last name, descending" => MenuChoice.LastNameDesc,
            _ => MenuChoice.Oopsie
        };
    }

    public static bool AskYesOrNo(string message)
    {
        string stringChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(message)
            .PageSize(3)
            .AddChoices(new[]
                {
                    "Yes",
                    "No"
                }
            ));
        return stringChoice == "Yes";
    }
}