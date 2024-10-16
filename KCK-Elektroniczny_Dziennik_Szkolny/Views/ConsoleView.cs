using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using KCK_Elektroniczny_Dziennik_Szkolny.Views;
using System;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class ConsoleView
    {
        private SchoolController controller;
        private GradeController gradeController;
        private GradeView gradeView;

        private string[] menuItems = new string[]
        {
            "1. Add class",
            "2. Add student",
            "3. Add teacher",
            "4. Add subject",
            "5. Manage grades",
            "6. View classes",
            "7. Exit"
        };

        public ConsoleView(SchoolController controller, GradeController gradeController)
        {
            this.controller = controller;
            this.gradeController = gradeController;
            this.gradeView = new GradeView(gradeController);
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
                    AddSubject();
                    break;
                case 4:
                    gradeView.DisplayGradeMenu();
                    break;
                case 5:
                    DisplayClasses();
                    break;
                case 6:
                    Console.WriteLine("Program ended.");
                    break;
            }
        }

        private void AddClass()
        {
            Console.Clear();
            Console.WriteLine("Enter class grade:");
            int grade = int.Parse(Console.ReadLine());

            var teachers = controller.GetTeachers();
            if (teachers.Count == 0) {
            Console.WriteLine("No teachers added");
                Console.ReadKey();
                return;
            }
            int selectedIndex = 0;
            bool selectingTeacher = true;


            while (selectingTeacher)
            {
                Console.Clear();
                Console.WriteLine("Select a supervising teacger");

                for (int i = 0; i < teachers.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"{teachers[i].Name} {teachers[i].Surname}");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? teachers.Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == teachers.Count - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selectingTeacher = false;
                        break;
                }
            }

            var selectedTeacher = teachers[selectedIndex];

            Class newClass = new Class { Grade = grade, SupervisingTeacher=selectedTeacher };
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

        private void AddSubject()
        {
            Console.Clear();
            Console.WriteLine("Enter subject's name:");
            string name = Console.ReadLine();

            var teachers = controller.GetTeachers();
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers added");
                Console.ReadKey();
                return;
            }
            int selectedIndex = 0;
            bool selectingTeacher = true;


            while (selectingTeacher)
            {
                Console.Clear();
                Console.WriteLine("Select teachers");

                for (int i = 0; i < teachers.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"{teachers[i].Name} {teachers[i].Surname}");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? teachers.Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == teachers.Count - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selectingTeacher = false;
                        break;
                }
            }

            var selectedTeacher = teachers[selectedIndex];

            Subject newSubject = new Subject { Name = name, Teacher = selectedTeacher };
            controller.AddSubject(newSubject);
            Console.WriteLine("Subject has been added.");
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
