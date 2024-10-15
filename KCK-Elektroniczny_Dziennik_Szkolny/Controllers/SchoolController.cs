﻿using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System.Collections.Generic;
using System.Linq;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Controllers
{
    public class SchoolController
    {
        private readonly ApplicationDbContext _context;

        public SchoolController(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddClass(Class schoolClass)
        {
            _context.Classes.Add(schoolClass);
            _context.SaveChanges();
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        public List<Class> GetClasses()
        {
            return _context.Classes.ToList();
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public List<Teacher> GetTeachers()
        {
            return _context.Teachers.ToList();
        }
    }
}
