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
        private Teacher loggedInTeacher;
        private int loggedInUserId;
        private MessageController messageController;
        private MessageView messageView;


        private string[] menuItems = new string[]
        {
            "1. Add class",
            "2. Add student",
            "3. Add teacher",
            "4. Add subject",
            "5. Add parent",
            "6. Manage grades",
            "7. View classes",
            "8. Inbox",
            "9. Compose Message",
            "10. Exit"
        };

        public ConsoleView(SchoolController controller, GradeController gradeController, UserController userController, Teacher loggedInTeacher, Parent loggedInParent, int loggedInUserId, MessageController messageController)
        {
            this.controller = controller;
            this.gradeController = gradeController;
            this.gradeView = new GradeView(gradeController, userController, loggedInTeacher, loggedInParent);
            this.loggedInUserId = loggedInUserId;
            this.messageController = messageController;
            this.messageView = new MessageView(messageController, userController, loggedInUserId);
        }


        public void DisplayMenu()
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
                        HandleSelection(currentSelection, ref running);
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

        private void HandleSelection(int selectedOption, ref bool running)
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
                    AddParent();
                    break;
                case 5:
                    gradeView.DisplayGradeMenu();
                    break;
                case 6:
                    DisplayClasses();
                    break;
                case 7:
                    messageView.DisplayInbox();
                    break;
                case 8:
                    messageView.ComposeMessage();
                    break;
                case 9:
                    Console.Clear();
                    Console.WriteLine("Exiting program...");
                    System.Threading.Thread.Sleep(1000);
                    running = false;
                    break;
            }
        }

        private void AddClass()
        {
            Console.Clear();
            Console.WriteLine("Select class grade (1-8):");

            int[] grades = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int selectedGradeIndex = 0;
            bool selectingGrade = true;

            while (selectingGrade)
            {
                Console.Clear();
                Console.WriteLine("Select class grade (1-8):");

                for (int i = 0; i < grades.Length; i++)
                {
                    if (i == selectedGradeIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(grades[i]);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedGradeIndex = (selectedGradeIndex == 0) ? grades.Length - 1 : selectedGradeIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedGradeIndex = (selectedGradeIndex == grades.Length - 1) ? 0 : selectedGradeIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selectingGrade = false;
                        break;
                }
            }

            int selectedGrade = grades[selectedGradeIndex];

            Console.Clear();
            Console.WriteLine($"Selected grade: {selectedGrade}");
            Console.WriteLine("Enter the class letter (A-Z):");

            string className;
            while (true)
            {
                className = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(className) && className.Length == 1 && char.IsLetter(className[0]))
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a single letter (A-Z):");
            }

            var teachers = controller.GetTeachers();
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers added");
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            int selectedTeacherIndex = 0;
            bool selectingTeacher = true;

            while (selectingTeacher)
            {
                Console.Clear();
                Console.WriteLine("Select a supervising teacher");

                for (int i = 0; i < teachers.Count; i++)
                {
                    if (i == selectedTeacherIndex)
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
                        selectedTeacherIndex = (selectedTeacherIndex == 0) ? teachers.Count - 1 : selectedTeacherIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedTeacherIndex = (selectedTeacherIndex == teachers.Count - 1) ? 0 : selectedTeacherIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selectingTeacher = false;
                        break;
                }
            }

            var selectedTeacher = teachers[selectedTeacherIndex];

            Class newClass = new Class { Grade = selectedGrade, Name = className, SupervisingTeacher = selectedTeacher };
            controller.AddClass(newClass);
            Console.WriteLine("Class has been added.");
            Console.ReadKey();
            DisplayMenu();
        }


        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("Enter student's first name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter student's last name:");
            string surname = Console.ReadLine();
            Console.WriteLine("Enter student's password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter student's BirthDate (yyyy-MM-dd):");
            DateOnly birthDate;

            while (!DateOnly.TryParse(Console.ReadLine(), out birthDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd):");
            }

            var classes = controller.GetClasses();
            if (classes.Count == 0)
            {
                Console.WriteLine("No classes available.");
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            int selectedIndex = 0;
            bool selecting = true;

            while (selecting)
            {
                Console.Clear();
                Console.WriteLine("Select a class for the student using arrow keys:");

                for (int i = 0; i < classes.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{i + 1}. Class {classes[i].Grade}");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? classes.Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == classes.Count - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selecting = false;
                        break;
                }
            }

            var selectedClass = classes[selectedIndex];

            Console.WriteLine("Enter parent's ID:");
            int parentId;
            while (!int.TryParse(Console.ReadLine(), out parentId))
            {
                Console.WriteLine("Invalid parent ID. Please enter a valid parent ID:");
            }

            var parent = controller.GetParentById(parentId);
            if (parent == null)
            {
                Console.WriteLine("Parent not found.");
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            Student newStudent = new Student { Name = name, Surname = surname, Password = password, BirthDate = birthDate, Parent = parent, Class = selectedClass };

            controller.AddStudent(newStudent);
            Console.WriteLine("Student has been added.");
            Console.ReadKey();
            DisplayMenu();
        }



        private void AddParent()
        {
            Console.Clear();

            Console.WriteLine("Enter parent's first name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter parent's last name:");
            string surname = Console.ReadLine();

            Console.WriteLine("Enter parent's password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter parent's email address:");
            string email;
            while (!IsValidEmail(Console.ReadLine(), out email))
            {
                Console.WriteLine("Invalid email address. Please enter a valid email:");
            }

            Console.WriteLine("Enter parent's phone number:");
            string phoneNumber;
            while (!IsValidPhoneNumber(Console.ReadLine(), out phoneNumber))
            {
                Console.WriteLine("Invalid phone number. The phone number must contain at least 9 digits:");
            }

            Parent newParent = new Parent
            {
                Name = name,
                Surname = surname,
                Password = password,
                Email = email,
                PhoneNumber = phoneNumber
            };

            controller.AddParent(newParent);

            Console.WriteLine("Parent has been added.");
            Console.ReadKey();
            DisplayMenu();
        }

        private bool IsValidEmail(string input, out string email)
        {
            email = input;
            var emailCheck = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            return emailCheck.IsValid(input);
        }

        private bool IsValidPhoneNumber(string input, out string phoneNumber)
        {
            phoneNumber = input;
            if (input.Length >= 9 && input.Length <= 14)
            {
                return true;
            }
            return false;
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
            DisplayMenu();
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
                Console.WriteLine("Select a class to view its students:");
                int selectedIndex = 0;
                bool selectingClass = true;

                while (selectingClass)
                {
                    Console.Clear();
                    Console.WriteLine("Select a class to view its students:\n");

                    for (int i = 0; i < classes.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.WriteLine($"Class: {classes[i].Grade} {classes[i].Name}");
                        Console.ResetColor();
                    }

                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex == 0) ? classes.Count - 1 : selectedIndex - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex == classes.Count - 1) ? 0 : selectedIndex + 1;
                            break;
                        case ConsoleKey.Enter:
                            selectingClass = false;
                            break;
                    }
                }
                var selectedClass = classes[selectedIndex];

                var students = controller.GetStudentsByClassId(selectedClass.Id);

                Console.Clear();
                Console.WriteLine($"Class {selectedClass.Grade} {selectedClass.Name} - Students:\n");

                if (students.Count == 0)
                {
                    Console.WriteLine("No students in this class.");
                }
                else
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"{student.Name} {student.Surname}");
                    }
                }
            }

            Console.ReadKey();
            DisplayMenu();
        }

    }
}
