using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
        public class ConsoleView
        {
            private SchoolController controller;

            private string[] menuItems = new string[]
            {
            "1. Dodaj klasę",
            "2. Dodaj ucznia",
            "3. Dodaj nauczyciela",
            "4. Wyświetl klasy",
            "5. Wyjście"
            };

            public ConsoleView(SchoolController controller)
            {
                this.controller = controller;
            }

            public void DisplayMenu()
            {
                int currentSelection = 0;
                bool running = true;

                while (running)
                {
                    Console.Clear();
                    Console.WriteLine("Elektroniczny Dziennik Szkolny\n");

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

                    var key = Console.ReadKey(true).Key;

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
                }
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
                        Console.WriteLine("Zakończono program.");
                        break;
                }
            }

            private void AddClass()
            {
                Console.Clear();
                Console.WriteLine("Podaj numer klasy:");
                int grade = int.Parse(Console.ReadLine());
                Class newClass = new Class { Grade = grade };
                controller.AddClass(newClass);
                Console.WriteLine("Klasa została dodana.");
                Console.ReadKey();
            }

            private void AddStudent()
            {
                Console.Clear();
                Console.WriteLine("Podaj imię ucznia:");
                string name = Console.ReadLine();
                Console.WriteLine("Podaj nazwisko ucznia:");
                string surname = Console.ReadLine();
                Student newStudent = new Student { Name = name, Surname = surname };
                controller.AddStudent(newStudent);
                Console.WriteLine("Uczeń został dodany.");
                Console.ReadKey();
            }

            private void AddTeacher()
            {
                Console.Clear();
                Console.WriteLine("Podaj imię nauczyciela:");
                string name = Console.ReadLine();
                Console.WriteLine("Podaj nazwisko nauczyciela:");
                string surname = Console.ReadLine();
                Teacher newTeacher = new Teacher { Name = name, Surname = surname };
                controller.AddTeacher(newTeacher);
                Console.WriteLine("Nauczyciel został dodany.");
                Console.ReadKey();
            }

            private void DisplayClasses()
            {
                Console.Clear();
                var classes = controller.GetClasses();
                if (classes.Count == 0)
                {
                    Console.WriteLine("Brak klas.");
                }
                else
                {
                    foreach (var c in classes)
                    {
                        Console.WriteLine($"Klasa: {c.Grade}");
                    }
                }
                Console.ReadKey();
            }
        }
    }
