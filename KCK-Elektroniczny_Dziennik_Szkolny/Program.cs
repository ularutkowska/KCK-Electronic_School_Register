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

                ChooseLanguage();

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


                static void ChooseLanguage()
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Select Language / Wybierz język:");
                        Console.WriteLine("1. English");
                        Console.WriteLine("2. Polski");

                        string input = Console.ReadLine();

                        if (input == "1")
                        {
                            LanguageManager.SetLanguage("en");
                            Console.WriteLine("Language set to English.");
                            break; 
                        }
                        else if (input == "2")
                        {
                            LanguageManager.SetLanguage("pl");
                            Console.WriteLine("Język ustawiony na Polski.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice / Nieprawidłowy wybór. Please enter 1 or 2 and press Enter.");
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
               
            }
        }
    }
}
