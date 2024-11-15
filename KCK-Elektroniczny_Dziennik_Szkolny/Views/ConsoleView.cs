using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models;
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
        private UserController userController;
        private string[] menuItems;


        private string[] teacherMenuItems = new string[]
        {
            LanguageManager.GetString("Menu_AddClass"),
            LanguageManager.GetString("Menu_AddStudent"),
            LanguageManager.GetString("Menu_AddTeacher"),
            LanguageManager.GetString("Menu_AddSubject"),
            LanguageManager.GetString("Menu_AddParent"),
            LanguageManager.GetString("Menu_ManageGrades"),
            LanguageManager.GetString("Menu_ViewClasses"),
            LanguageManager.GetString("Menu_Inbox"),
            LanguageManager.GetString("Menu_SentMessages"),
            LanguageManager.GetString("Menu_ComposeMessage"),
            LanguageManager.GetString("Menu_Exit")
        };

        private string[] parentMenuItems = new string[]
        {
            LanguageManager.GetString("Menu_ViewClasses"),
            LanguageManager.GetString("Menu_Inbox"),
            LanguageManager.GetString("Menu_SentMessages"),
            LanguageManager.GetString("Menu_ComposeMessage"),
            LanguageManager.GetString("Menu_ManageGrades"),
            LanguageManager.GetString("Menu_Exit")
        };

        private string[] studentMenuItems = new string[]
        {
            LanguageManager.GetString("Menu_ViewClasses"),
            LanguageManager.GetString("Menu_Inbox"),
            LanguageManager.GetString("Menu_SentMessages"),
            LanguageManager.GetString("Menu_ManageGrades"),
            LanguageManager.GetString("Menu_Exit")
        };

        public ConsoleView(SchoolController controller, GradeController gradeController, UserController userController, Teacher loggedInTeacher, Parent loggedInParent, int loggedInUserId, MessageController messageController)
        {
            this.controller = controller;
            this.gradeController = gradeController;
            this.gradeView = new GradeView(gradeController, userController, loggedInTeacher, loggedInParent);
            this.loggedInUserId = loggedInUserId;
            this.messageController = messageController;
            this.messageView = new MessageView(messageController, userController, loggedInUserId);
            this.userController = userController;
        }


        public void DisplayMenu()
        {
            string userRole = userController.GetLoggedInRole();
            switch (userRole)
            {
                case "Teacher":
                    menuItems = teacherMenuItems;
                    break;
                case "Parent":
                    menuItems = parentMenuItems;
                    break;
                case "Student":
                    menuItems = studentMenuItems;
                    break;
                default:
                    Console.WriteLine("Unknown role. Exiting...");
                    return;
            }

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
            }
        }

        private void DrawMenu(int currentSelection)
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("App_Title"));

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

        private void HandleSelection(int selectedOption, ref bool running)
        {
            switch (userController.GetLoggedInRole())
            {
                case "Teacher":
                    HandleTeacherSelection(selectedOption, ref running);
                    break;
                case "Parent":
                    HandleParentSelection(selectedOption, ref running);
                    break;
                case "Student":
                    HandleStudentSelection(selectedOption, ref running);
                    break;
            }
        }

        private void HandleTeacherSelection(int selectedOption, ref bool running)
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
                    messageView.DisplaySentMessages();
                    break;
                case 9:
                    messageView.ComposeMessage();
                    break;
                case 10:
                    ExitProgram(ref running);
                    break;
            }
        }

        private void HandleParentSelection(int selectedOption, ref bool running)
        {
            switch (selectedOption)
            {
                case 0:
                    DisplayClasses();
                    break;
                case 1:
                    messageView.DisplayInbox();
                    break;
                case 2:
                    messageView.DisplaySentMessages();
                    break;
                case 3:
                    messageView.ComposeMessage();
                    break;
                case 4:
                    gradeView.DisplayGradeMenu();
                    break;
                case 5:
                    ExitProgram(ref running);
                    break;
            }
        }

        private void HandleStudentSelection(int selectedOption, ref bool running)
        {
            switch (selectedOption)
            {
                case 0:
                    DisplayClasses();
                    break;
                case 1:
                    messageView.DisplayInbox();
                    break;
                case 2:
                    messageView.DisplaySentMessages();
                    break;
                case 3:
                    gradeView.DisplayGradeMenu();
                    break;
                case 4:
                    ExitProgram(ref running);
                    break;
            }
        }

        private void ExitProgram(ref bool running)
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("App_ExitMessage"));
            System.Threading.Thread.Sleep(1000);
            running = false;
        }


        private void AddClass()
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("Prompt_SelectClassGrade"));

            int[] grades = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int selectedGradeIndex = 0;
            bool selectingGrade = true;

            while (selectingGrade)
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetString("Prompt_SelectClassGrade"));

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
            Console.Write(LanguageManager.GetString("SelectedGrade"));
            Console.WriteLine(selectedGrade);
            Console.WriteLine(LanguageManager.GetString("Prompt_ClassLetter"));

            string className;
            while (true)
            {
                className = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(className) && className.Length == 1 && char.IsLetter(className[0]))
                {
                    break;
                }
                Console.WriteLine(LanguageManager.GetString("InvalidInputInfo"));
            }

            var teachers = controller.GetTeachers();
            if (teachers.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("NoTeachersInfo"));
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            int selectedTeacherIndex = 0;
            bool selectingTeacher = true;

            while (selectingTeacher)
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetString("Prompt_SupervisingTeacher"));

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
            Console.WriteLine(LanguageManager.GetString("AddedClassInfo"));
            Console.ReadKey();
            DisplayMenu();
        }


        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("Prompt_StudentFirstName"));
            string name = Console.ReadLine();
            Console.WriteLine(LanguageManager.GetString("Prompt_StudentLastName"));
            string surname = Console.ReadLine();
            Console.WriteLine(LanguageManager.GetString("Prompt_StudentPassword"));
            string password = Console.ReadLine();
            Console.WriteLine(LanguageManager.GetString("Prompt_StudentBirthDate"));
            DateOnly birthDate;

            while (!DateOnly.TryParse(Console.ReadLine(), out birthDate))
            {
                Console.WriteLine(LanguageManager.GetString("Error_InvalidDateFormat"));
            }

            var classes = controller.GetClasses();
            if (classes.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("NoClassesInfo"));
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            int selectedIndex = 0;
            bool selecting = true;

            while (selecting)
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetString("Prompt_SelectClass"));

                for (int i = 0; i < classes.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(string.Format(LanguageManager.GetString("Menu_Class"), i + 1, classes[i].Grade));
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

            Console.WriteLine(LanguageManager.GetString("Prompt_EnterParentLastName"));
            String parentLastNameInput;
            while (true)
            {
                parentLastNameInput= Console.ReadLine();
                if (parentLastNameInput.Length >= 3)
                {
                    break;
                }
                Console.WriteLine(LanguageManager.GetString("Prompt_EnterAtLeast3Letters"));
            }

            var matchingParents = controller.SearchParentsByLastName(parentLastNameInput);
            if(matchingParents.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("Message_NoParentsFound"));
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            selectedIndex = 0;
            selecting = true;

            while (selecting)
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetString("Message_SelectParent"));

                for (int i = 0; i < matchingParents.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(string.Format(LanguageManager.GetString("Message_ParentInfo"), i + 1, matchingParents[i].Name, matchingParents[i].Surname, matchingParents[i].PhoneNumber));
                    Console.ResetColor();
                }
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? matchingParents.Count - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == matchingParents.Count - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selecting = false;
                        break;
                }
            }

            var selectedParent = matchingParents[selectedIndex];

            Student newStudent = new Student{Name = name, Surname = surname, Password = password,BirthDate = birthDate, Parent = selectedParent, Class = selectedClass};

            controller.AddStudent(newStudent);
            Console.WriteLine(LanguageManager.GetString("Message_StudentAdded"));
            Console.ReadKey();
            DisplayMenu();
        }



        private void AddParent()
        {
            Console.Clear();

            Console.WriteLine(LanguageManager.GetString("Prompt_ParentFirstName"));
            string name = Console.ReadLine();

            Console.WriteLine(LanguageManager.GetString("Prompt_ParentLastName"));
            string surname = Console.ReadLine();

            Console.WriteLine(LanguageManager.GetString("Prompt_ParentPassword"));
            string password = Console.ReadLine();

            Console.WriteLine(LanguageManager.GetString("Prompt_ParentEmail"));
            string email;
            while (!IsValidEmail(Console.ReadLine(), out email))
            {
                Console.WriteLine(LanguageManager.GetString("Error_InvalidEmail"));
            }

            Console.WriteLine(LanguageManager.GetString("Prompt_ParentPhoneNumber"));
            string phoneNumber;
            while (!IsValidPhoneNumber(Console.ReadLine(), out phoneNumber))
            {
                Console.WriteLine(LanguageManager.GetString("Error_InvalidPhoneNumber"));
            }

            Parent newParent = new Parent{Name = name, Surname = surname, Password = password, Email = email, PhoneNumber = phoneNumber};

            controller.AddParent(newParent);

            Console.WriteLine(LanguageManager.GetString("Message_ParentAdded"));
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
            Console.WriteLine(LanguageManager.GetString("Prompt_EnterSubjectName"));
            string name = Console.ReadLine();

            var teachers = controller.GetTeachers();
            if (teachers.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("NoTeachersInfo"));
                Console.ReadKey();
                return;
            }
            int selectedIndex = 0;
            bool selectingTeacher = true;


            while (selectingTeacher)
            {
                Console.Clear();
                Console.WriteLine(LanguageManager.GetString("Prompt_SelectTeachers"));

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
            Console.WriteLine(LanguageManager.GetString("Message_SubjectAdded"));
            Console.ReadKey();
        }       

        private void AddTeacher()
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("Message_EnterTeacherFirstName"));
            string name = Console.ReadLine();
            Console.WriteLine(LanguageManager.GetString("Message_EnterTeacherLastName"));
            string surname = Console.ReadLine();
            Console.WriteLine(LanguageManager.GetString("Message_EnterTeacherPassword"));
            string password = Console.ReadLine();
            Console.WriteLine(LanguageManager.GetString("Message_EnterTeacherEmail"));
            string email = Console.ReadLine();
            Console.WriteLine(LanguageManager.GetString("Message_EnterTeacherPhone"));
            string phone = Console.ReadLine();
            Teacher newTeacher = new Teacher { Name = name, Surname = surname, Password = password, Email = email, PhoneNumber = phone };
            controller.AddTeacher(newTeacher);
            Console.WriteLine(LanguageManager.GetString("Message_TeacherAdded"));
            Console.ReadKey();
            DisplayMenu();
        }

        private void DisplayClasses()
        {
            Console.Clear();
            var classes = controller.GetClasses();
            if (classes.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("Message_NoClassesAvailable"));
            }
            else
            {
                Console.WriteLine(LanguageManager.GetString("Message_SelectClassToView"));
                int selectedIndex = 0;
                bool selectingClass = true;

                while (selectingClass)
                {
                    Console.Clear();
                    Console.WriteLine(LanguageManager.GetString("Message_SelectClassToView"));

                    for (int i = 0; i < classes.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(LanguageManager.GetString("Class"));
                        Console.WriteLine(classes[i].Grade + classes[i].Name);
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
                Console.Write(LanguageManager.GetString("Class"));
                Console.Write(selectedClass.Grade + selectedClass.Name);
                Console.WriteLine(LanguageManager.GetString("Students_Info"));

                if (students.Count == 0)
                {
                    Console.WriteLine(LanguageManager.GetString("Message_NoStudentsInClass"));
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
