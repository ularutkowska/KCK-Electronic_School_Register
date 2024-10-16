using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Views;

namespace KCK_Elektroniczny_Dziennik_Szkolny
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                UserController userController = new UserController(context);
                SchoolController schoolController = new SchoolController(context);
                GradeController gradeController = new GradeController(context);

                UserView userView = new UserView(userController);
                ConsoleView consoleView = new ConsoleView(schoolController, gradeController);
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
}
