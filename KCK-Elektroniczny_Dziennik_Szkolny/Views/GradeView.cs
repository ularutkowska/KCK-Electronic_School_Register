using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class GradeView
    {
        private GradeController controller;

        private string[] menuItems = new string[]
        {
            "1. Add Grade",
            "2. View Grades",
            "3. Exit"
        };

        public GradeView(GradeController controller)
        {
            this.controller = controller;
        }

        public void DisplayGradeMenu()
        {
            int currentSelection = 0;
            bool running = true;

            while (running)
            {
                DrawMenu(currentSelection);

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
            Console.WriteLine("Grade Management\n");

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
            Console.SetCursorPosition(0, previousSelection + 1);
            Console.ResetColor();
            Console.WriteLine(menuItems[previousSelection] + " ");

            Console.SetCursorPosition(0, currentSelection + 1);
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
                    AddGrade();
                    break;
                case 1:
                    ViewGrades();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Exiting grade management.");
                    System.Threading.Thread.Sleep(1000);
                    Environment.Exit(0);  // Exit program or just stop GradeView
                    break;
            }
        }

        private void AddGrade()
        {
            Console.Clear();

            try
            {
                Console.WriteLine("Enter grade value (1-6):");
                int value = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter subject ID:");
                int subjectId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter teacher ID:");
                int teacherId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter student ID:");
                int studentId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter date (YYYY-MM-DD):");
                DateTime date = DateTime.Parse(Console.ReadLine());

                var subject = controller.GetSubjectById(subjectId);
                var teacher = controller.GetTeacherById(teacherId);
                var student = controller.GetStudentById(studentId);

                if (subject == null || teacher == null || student == null)
                {
                    Console.WriteLine("Invalid subject, teacher, or student ID.");
                    return;
                }

                Grade newGrade = new Grade { Value = value, Subject = subject, Teacher = teacher, Student = student, Date = date };
                controller.AddGrade(newGrade);

                Console.WriteLine("Grade has been added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void ViewGrades()
        {
            Console.Clear();
            Console.WriteLine("Enter student ID to view their grades:");
            int studentId = int.Parse(Console.ReadLine());

            var grades = controller.GetGradesForStudent(studentId);

            if (grades.Count == 0)
            {
                Console.WriteLine("No grades found for this student.");
            }
            else
            {
                Console.WriteLine($"Grades for student ID {studentId}:");

                foreach (var grade in grades)
                {
                    Console.WriteLine($"Subject: {grade.Subject.Name}, Grade: {grade.Value}, Teacher: {grade.Teacher.Name}, Date: {grade.Date.ToShortDateString()}");
                }
            }
            Console.ReadKey();
        }
    }
}
