using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System;
using System.Collections.Generic;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class GradeView
    {
        private GradeController controller;
        private UserController userController;
        private Teacher loggedInTeacher;
        private Parent loggedInParent;

        private string[] menuItems = new string[]
        {
            "1. View by Class",
            "2. View Personal (Parent)",
            "3. Exit"
        };

        public GradeView(GradeController controller, UserController userController, Teacher loggedInTeacher, Parent loggedInParent)
        {
            this.controller = controller;
            this.userController = userController;
            this.loggedInTeacher = loggedInTeacher;
            this.loggedInParent = loggedInParent;
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
                    ViewGradesByClass();
                    break;
                case 1:
                    if (loggedInParent != null)
                    {
                        ViewPersonalGrades(loggedInParent);
                    }
                    else
                    {
                        Console.WriteLine("You are not logged in as a parent.");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Exiting grade management.");
                    System.Threading.Thread.Sleep(1000);
                    return;
            }
        }

        private void ViewGradesByClass()
        {
            Console.Clear();

            var classes = controller.GetClasses();
            if (classes.Count == 0)
            {
                Console.WriteLine("No classes available.");
                Console.ReadKey();
                return;
            }

            int selectedClassIndex = 0;
            bool selectingClass = true;

            while (selectingClass)
            {
                Console.Clear();
                Console.WriteLine("Select a class:");

                for (int i = 0; i < classes.Count; i++)
                {
                    if (i == selectedClassIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"Class: {classes[i].Grade}");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedClassIndex = (selectedClassIndex == 0) ? classes.Count - 1 : selectedClassIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedClassIndex = (selectedClassIndex == classes.Count - 1) ? 0 : selectedClassIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selectingClass = false;
                        break;
                }
            }

            var selectedClass = classes[selectedClassIndex];
            ViewStudentsInClass(selectedClass);
        }

        private void ViewStudentsInClass(Class selectedClass)
        {
            var students = controller.GetStudentsByClassId(selectedClass.Id);

            if (students.Count == 0)
            {
                Console.WriteLine("No students in this class.");
                Console.ReadKey();
                return;
            }

            int selectedStudentIndex = 0;
            bool selectingStudent = true;

            while (selectingStudent)
            {
                Console.Clear();
                Console.WriteLine($"Class {selectedClass.Grade}: Select a student:");

                for (int i = 0; i < students.Count; i++)
                {
                    if (i == selectedStudentIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"{students[i].Name} {students[i].Surname}");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedStudentIndex = (selectedStudentIndex == 0) ? students.Count - 1 : selectedStudentIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedStudentIndex = (selectedStudentIndex == students.Count - 1) ? 0 : selectedStudentIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selectingStudent = false;
                        break;
                }
            }

            var selectedStudent = students[selectedStudentIndex];
            DisplayGradesForStudent(selectedStudent);
        }

        private void ViewPersonalGrades(Parent loggedInParent)
        {
            var student = controller.GetStudentByParentId(loggedInParent.Id);
            if (student != null)
            {
                DisplayGradesForStudent(student);
            }
            else
            {
                Console.WriteLine("No student linked to this parent.");
                Console.ReadKey();
            }
        }

        private void DisplayGradesForStudent(Student student)
        {
            var grades = controller.GetGradesForStudent(student.Id);

            if (grades.Count == 0)
            {
                Console.WriteLine($"No grades found for {student.Name} {student.Surname}.");
            }
            else
            {
                Console.WriteLine($"Grades for {student.Name} {student.Surname}:");

                foreach (var grade in grades)
                {
                    Console.WriteLine($"Subject: {grade.Subject.Name}, Grade: {grade.Value}, Teacher: {grade.Teacher.Name}, Date: {grade.Date.ToShortDateString()}");
                }
            }
            Console.ReadKey();
        }
    }
}
