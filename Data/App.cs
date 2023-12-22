namespace Labb3.Data;

internal class App
{
    public MenuManager MenuManager { get; set; } = new();
    public void Run()
    {
        MenuManager.GetChoice();
    }
}