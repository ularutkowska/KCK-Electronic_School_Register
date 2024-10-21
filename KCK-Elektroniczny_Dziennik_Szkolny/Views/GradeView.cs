using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using Microsoft.Identity.Client;
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
            "1. Add Grade",
            "2. View by Class",
            "3. View Personal (Parent)",
            "4. Exit"
        };

        private bool exitGradeMenu = false;

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
            exitGradeMenu = false;

            while (!exitGradeMenu)
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
                    ViewGradesByClass();
                    break;
                case 2:
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
                case 3:
                    Console.Clear();
                    Console.WriteLine("Exiting grade management.");
                    Thread.Sleep(1000);
                    exitGradeMenu = true;
                    return;
            }
        }
        private void AddGrade()
        {
            Console.Clear();

            if (loggedInTeacher == null)
            {
                Console.WriteLine("You are not logged in as a teacher.");
                Console.ReadKey();
                return;
            }

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
                    Console.WriteLine($"Class {classes[i].Grade}");
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

            var subjects = controller.GetSubjectsByTeacherId(loggedInTeacher.Id);
            if (subjects.Count == 0)
            {
                Console.WriteLine("You have no assigned subjects.");
                Console.ReadKey();
                return;
            }

            int selectedSubjectIndex = 0;
            bool selectingSubject = true;

            while (selectingSubject)
            {
                Console.Clear();
                Console.WriteLine("Select a subject:");

                for (int i = 0; i < subjects.Count; i++)
                {
                    if (i == selectedSubjectIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(subjects[i].Name);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedSubjectIndex = (selectedSubjectIndex == 0) ? subjects.Count - 1 : selectedSubjectIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedSubjectIndex = (selectedSubjectIndex == subjects.Count - 1) ? 0 : selectedSubjectIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selectingSubject = false;
                        break;
                }
            }

            var selectedSubject = subjects[selectedSubjectIndex];

            Console.Clear();
            Console.WriteLine($"Adding a grade for {selectedStudent.Name} {selectedStudent.Surname} in {selectedSubject.Name}:");
            Console.WriteLine("Enter the grade value (1-6):");
            int gradeValue;

            while (!int.TryParse(Console.ReadLine(), out gradeValue) || gradeValue < 1 || gradeValue > 6)
            {
                Console.WriteLine("Invalid grade. Please enter a value between 1 and 6:");
            }

            Grade newGrade = new Grade
            {
                Value = gradeValue,
                Subject = selectedSubject,
                Teacher = loggedInTeacher,
                Student = selectedStudent,
                Date = DateTime.Now
            };

            controller.AddGrade(newGrade);

            Console.WriteLine("Grade has been added successfully.");
            Console.ReadKey();
            DisplayGradeMenu();
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
