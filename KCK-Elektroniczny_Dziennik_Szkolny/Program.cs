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
                /*
                while (true) // Pętla, aby pozwolić na powtórne wywołanie
                {
                    Console.Clear();
                    Console.WriteLine("Select Language / Wybierz język");
                    Console.WriteLine("1. English");
                    Console.WriteLine("2. Polski");

                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.D1:
                            LanguageManager.SetLanguage("en");
                            return; // Zakończ pętlę
                        case ConsoleKey.D2:
                            LanguageManager.SetLanguage("pl");
                            return; // Zakończ pętlę
                        default:
                            Console.WriteLine(LanguageManager.GetString("InvalidChoice")); // Użyj lokalizowanego komunikatu
                            Console.ReadKey(); // Czekaj na naciśnięcie klawisza, aby użytkownik mógł zobaczyć komunikat
                            break;
                    }
                } */

                UserController userController = new UserController(context);
                SchoolController schoolController = new SchoolController(context);
                GradeController gradeController = new GradeController(context);
                MessageController messageController = new MessageController(context);

                UserView userView = new UserView(userController);
                bool exitProgram = false;

                while (!exitProgram)
                {
                    bool loggedIn = userView.LoginScreen();

                    if (loggedIn)
                    {
                        var loggedInTeacher = userController.GetLoggedInTeacher();
                        var loggedInParent = userController.GetLoggedInParent();
                        var loggedInStudent = userController.GetLoggedInStudent();

                        int loggedInUserId = 0;
                        if (loggedInTeacher != null)
                        {
                            loggedInUserId = loggedInTeacher.Id;
                        }
                        else if (loggedInParent != null)
                        {
                            loggedInUserId = loggedInParent.Id;
                        }
                        else if (loggedInStudent != null)
                        {
                            loggedInUserId = loggedInStudent.Id;
                        }

                        Console.Clear();
                        Console.WriteLine(LanguageManager.GetString("Message_LoginSuccess"));
                        System.Threading.Thread.Sleep(3000);

                        ConsoleView consoleView = new ConsoleView(schoolController, gradeController, userController, loggedInTeacher, loggedInParent, loggedInUserId, messageController);
                        consoleView.DisplayMenu();
                        exitProgram = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(LanguageManager.GetString("Message_ReturnToRoleSelection"));
                    }
                }
            }
        }
    }
}
