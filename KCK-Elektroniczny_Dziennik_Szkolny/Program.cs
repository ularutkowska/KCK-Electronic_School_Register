using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Views;

namespace KCK_Elektroniczny_Dziennik_Szkolny
{
    class Program
    {
        static void Main(string[] args)
        {
            UserController userController = new UserController();
            UserView userView = new UserView(userController);
            SchoolController schoolController = new SchoolController();
            ConsoleView consoleView = new ConsoleView(schoolController);
            bool exitProgram = false;

            while (!exitProgram)
            {
                bool loggedIn = userView.LoginScreen();

                if (loggedIn)
                {
                    Console.Clear();
                    Console.WriteLine("Logged in successfully.");
                    System.Threading.Thread.Sleep(3000);
                    consoleView.DisplayMenu();
                    exitProgram = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You chose to return to the role selection.");
                }
            }
        }
    }
}
