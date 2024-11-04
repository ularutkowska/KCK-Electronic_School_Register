using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using System;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class UserView
    {
        private UserController userController;
        private string[] roles = new string[] { "Teacher", "Parent", "Student" };
        private string loggedInRole;

        public UserView(UserController controller)
        {
            userController = controller;
        }

        public bool LoginScreen()
        {
            int selectedIndex = 0;
            bool loggedIn = false;

            while (!loggedIn)
            {
                DrawMenu(selectedIndex);
                ConsoleKey key = Console.ReadKey(true).Key;
                int previousIndex = selectedIndex;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? roles.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == roles.Length - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        string selectedRole = roles[selectedIndex];
                        loggedIn = HandleLogin(selectedRole);

                        if (!loggedIn)
                        {
                            Console.Clear();
                            Console.WriteLine(LanguageManager.GetString("InvalidLoginOrPassword"));
                            System.Threading.Thread.Sleep(2000);
                        }
                        break;
                }

                if (previousIndex != selectedIndex)
                {
                    UpdateMenuRow(previousIndex, selectedIndex);
                }
            }

            return loggedIn;
        }

        private void DrawMenu(int selectedIndex)
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("ChooseRole"));

            for (int i = 0; i < roles.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(LanguageManager.GetString(roles[i]));
                Console.ResetColor();
            }
        }

        private void UpdateMenuRow(int previousIndex, int selectedIndex)
        {
            Console.SetCursorPosition(0, previousIndex + 1);
            Console.ResetColor();
            Console.WriteLine(roles[previousIndex] + " ");

            Console.SetCursorPosition(0, selectedIndex + 1);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(roles[selectedIndex] + " ");
            Console.ResetColor();
        }

        private bool HandleLogin(string role)
        {
            bool retryLogin = true;

            while (retryLogin)
            {
                if (role == "Student")
                {
                    Console.Clear();
                    Console.WriteLine(LanguageManager.GetString("EnterStudentID"));
                    int studentId = int.Parse(Console.ReadLine());

                    Console.WriteLine(LanguageManager.GetString("EnterPassword"));
                    string password = Console.ReadLine();

                    bool success = userController.StudentLogin(studentId, password);

                    if (success)
                    {
                        loggedInRole = "Student";
                        Console.WriteLine(LanguageManager.GetString("LoggedInAsStudent"));
                        System.Threading.Thread.Sleep(1000);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(LanguageManager.GetString("InvalidIdOrPassword"));
                        System.Threading.Thread.Sleep(2000);
                        retryLogin = AskToRetry();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(LanguageManager.GetString("EnterEmailAddress"));
                    string email = Console.ReadLine();

                    Console.WriteLine(LanguageManager.GetString("EnterPassword"));
                    string password = Console.ReadLine();

                    bool success = userController.Login(email, password, role);

                    if (success)
                    {
                        loggedInRole = userController.GetLoggedInRole();
                        Console.WriteLine(LanguageManager.GetString("LoggedInAs") + LanguageManager.GetString(loggedInRole));
                        System.Threading.Thread.Sleep(1000);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(LanguageManager.GetString("InvalidLoginCredentials"));
                        System.Threading.Thread.Sleep(2000);
                        retryLogin = AskToRetry();
                    }
                }
            }

            return false;
        }

        private bool AskToRetry()
        {
            string[] options = { "Yes", "No" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetString("RetryPrompt"));

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(LanguageManager.GetString(options[i]));
                    Console.ResetColor();
                }

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        return options[selectedIndex] == "Yes";
                }
            }
        }

        public string GetLoggedInRole()
        {
            return loggedInRole;
        }
    }
}
