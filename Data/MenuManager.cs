namespace Labb3.Data;

internal class MenuManager
{
    public DbManager DbM { get; set; } = new();
    public MenuChoice Choice { get; set; } = MenuChoice.StartMenu;
    public bool Running = true;

    public void GetChoice()
    {
        while (Running)
        {
            switch (Choice)
            {
                case MenuChoice.StartMenu:
                    Choice = UserInterface.StartMenu();
                    break;

                case MenuChoice.StudentMenu:
                    Choice = UserInterface.ViewStudentsMenu();
                    break;

                case MenuChoice.FirstNameAsc:
                    DbM.GetAllStudentsSorted(Choice);
                    Choice = MenuChoice.StudentMenu;
                    break;

                case MenuChoice.FirstNameDesc:
                    DbM.GetAllStudentsSorted(Choice);
                    Choice = MenuChoice.StudentMenu;
                    break;

                case MenuChoice.LastNameAsc:
                    DbM.GetAllStudentsSorted(Choice);
                    Choice = MenuChoice.StudentMenu;
                    break;
                    
                case MenuChoice.LastNameDesc:
                    DbM.GetAllStudentsSorted(Choice);
                    Choice = MenuChoice.StudentMenu;
                    break;

                case MenuChoice.EmployeeMenu:
                    Choice = UserInterface.ViewEmployeesMenu();
                    break;

                case MenuChoice.All:
                    DbM.GetAllEmployeesByRole("All");
                    Choice = MenuChoice.EmployeeMenu;
                    break;

                case MenuChoice.Teacher:
                    DbM.GetAllEmployeesByRole("Teacher");
                    Choice = MenuChoice.EmployeeMenu;
                    break;

                case MenuChoice.Administrator:
                    DbM.GetAllEmployeesByRole("Administrator");
                    Choice = MenuChoice.EmployeeMenu;
                    break;

                case MenuChoice.Janitor:
                    DbM.GetAllEmployeesByRole("Janitor");
                    Choice = MenuChoice.EmployeeMenu;
                    break;
                    
                case MenuChoice.Principal:
                    DbM.GetAllEmployeesByRole("Principal");
                    Choice = MenuChoice.EmployeeMenu;
                    break;

                case MenuChoice.Classes:
                    var className = UserInterface.ClassMenu(DbM.GetAllClasses());
                    DbM.GetClassInfo(className);
                    Choice = MenuChoice.StartMenu;
                    break;

                case MenuChoice.Courses:
                    DbM.GetGrades();
                    Choice = MenuChoice.StartMenu;
                    break;

                case MenuChoice.Grades:
                    DbM.GetRecentGrades();
                    Choice = MenuChoice.StartMenu;
                    break;

                case MenuChoice.Registration:
                    Choice = UserInterface.CreationMenu();
                    break;

                case MenuChoice.CreateStudent:
                    DbM.AddPerson(Choice);
                    Choice = MenuChoice.Registration;
                    break;

                case MenuChoice.CreateEmployee:
                    DbM.AddPerson(Choice);
                    Choice = MenuChoice.Registration;
                    break;

                case MenuChoice.Back:
                    Choice = MenuChoice.StartMenu;
                    break;

                case MenuChoice.Exit:
                    Running = false;
                    break;

                case MenuChoice.Oopsie:
                    Console.WriteLine("Oopsie woopsie! Uwu we made a fucky wucky!" +
                                      "\nA wittle fucko boingo!\nThe code monkeys " +
                                      "at our HQ are working\nVEWY HAWD to fix this!");
                    break;
            }
        }
    }
}