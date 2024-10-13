// See https://aka.ms/new-console-template for more information
using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Views;
namespace KCK_Elektroniczny_Dziennik_Szkolny
{
    class Program
    {
        static void Main(string[] args)
        {
            SchoolController schoolController = new SchoolController();
            ConsoleView consoleView = new ConsoleView(schoolController);
            consoleView.DisplayMenu();
        }
    }
}