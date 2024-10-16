using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class ConsoleView
    {
        private SchoolController controller;

        private string[] menuItems = new string[]
        {
            "1. Add class",
            "2. Add student",
            "3. Add teacher",
            "4. View classes",
            "5. Exit"
        };

        public ConsoleView(SchoolController controller)
        {
            this.controller = controller;
        }

        public void DisplayMenu()
        {
            int currentSelection = 0;
            bool running = true;

            DrawMenu(currentSelection);

            while (running)
            {
                var key = Console.ReadKey(true).Key;

                int previousSelection = currentSelection;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentSelection = (currentSelection == 0) ? menuItems.Length - 1 : currentSelection - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentSelection = (currentSelection == menuItems.Length - 1) ? 0 : currentSelection + 1;
                        break;
                    case ConsoleKey.Enter:
                        HandleSelection(currentSelection);
                        if (currentSelection == menuItems.Length - 1) running = false;
                        break;
                }

                if (previousSelection != currentSelection)
                {
                    UpdateMenuRow(previousSelection, currentSelection);
                }
            }
        }

        private void DrawMenu(int currentSelection)
        {
            Console.Clear();
            Console.WriteLine("Electronic School Diary\n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == currentSelection)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(menuItems[i]);
                Console.ResetColor();
            }
        }

        private void UpdateMenuRow(int previousSelection, int currentSelection)
        {
            Console.SetCursorPosition(0, previousSelection + 2);
            Console.ResetColor();
            Console.WriteLine(menuItems[previousSelection] + " ");

            Console.SetCursorPosition(0, currentSelection + 2);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(menuItems[currentSelection] + " ");
            Console.ResetColor();
        }

        private void HandleSelection(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    AddClass();
                    break;
                case 1:
                    AddStudent();
                    break;
                case 2:
                    AddTeacher();
                    break;
                case 3:
                    DisplayClasses();
                    break;
                case 4:
                    Console.WriteLine("Program ended.");
                    break;
            }
        }

        private void AddClass()
        {
            Console.Clear();
            Console.WriteLine("Enter class grade:");
            int grade = int.Parse(Console.ReadLine());
            Class newClass = new Class { Grade = grade };
            controller.AddClass(newClass);
            Console.WriteLine("Class has been added.");
            Console.ReadKey();
        }

        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("Enter student's first name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter student's last name:");
            string surname = Console.ReadLine();
            Student newStudent = new Student { Name = name, Surname = surname };
            controller.AddStudent(newStudent);
            Console.WriteLine("Student has been added.");
            Console.ReadKey();
        }

        private void AddTeacher()
        {
            Console.Clear();
            Console.WriteLine("Enter teacher's first name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter teacher's last name:");
            string surname = Console.ReadLine();
            Console.WriteLine("Enter a new password for the teacher");
            string password = Console.ReadLine();
            Console.WriteLine("Enter the teacher's email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter the teacher's phone number");
            string phone = Console.ReadLine();
            Teacher newTeacher = new Teacher { Name = name, Surname = surname, Password = password, Email = email, PhoneNumber = phone };
            controller.AddTeacher(newTeacher);
            Console.WriteLine("Teacher has been added.");
            Console.ReadKey();
        }

        private void DisplayClasses()
        {
            Console.Clear();
            var classes = controller.GetClasses();
            if (classes.Count == 0)
            {
                Console.WriteLine("No classes available.");
            }
            else
            {
                foreach (var c in classes)
                {
                    Console.WriteLine($"Class: {c.Grade}");
                }
            }
            Console.ReadKey();
        }
    }
}
