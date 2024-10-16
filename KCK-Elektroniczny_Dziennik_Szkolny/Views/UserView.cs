using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using System;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class UserView
    {
        private UserController userController;
        private string[] roles = new string[] { "Teacher", "Parent", "Student" };

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
                            Console.WriteLine("Invalid login or password. Returning to role selection...");
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
            Console.WriteLine("Choose a role");

            for (int i = 0; i < roles.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(roles[i]);
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
                    Console.WriteLine("Enter student ID:");
                    int studentId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();

                    bool success = userController.StudentLogin(studentId, password);

                    if (success)
                    {
                        Console.WriteLine("Logged in as student.");
                        System.Threading.Thread.Sleep(1000);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID or password.");
                        System.Threading.Thread.Sleep(2000);
                        retryLogin = AskToRetry();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Enter email address:");
                    string email = Console.ReadLine();

                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();

                    bool success = userController.Login(email, password, role);

                    if (success)
                    {
                        Console.WriteLine($"Logged in as {role}.");
                        System.Threading.Thread.Sleep(1000);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid login credentials.");
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
                Console.WriteLine("Do you want to retry?");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(options[i]);
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
    }
}
