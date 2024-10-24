﻿using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Controllers
{
    public class GradeController
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            _context.SaveChanges();
        }
        public Subject GetSubjectById(int id)
        {
            return _context.Subjects.Find(id);
        }
        public List<Subject> GetSubjectsByTeacherId(int teacherId)
        {
            return _context.Subjects.Where(s => s.Teacher.Id == teacherId).ToList();
        }
        public Teacher GetTeacherById(int id)
        {
            return _context.Teachers.Find(id);
        }
        public Student GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }
        public List<Grade> GetGradesForStudent(int studentId)
        {
            return _context.Grades
                .Where(g => g.Student.Id == studentId)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .Include(g => g.Student)
                .ToList();
        }
        public List<Class> GetClasses()
        {
            return _context.Classes.ToList();
        }

        public List<Student> GetStudentsByClassId(int classId)
        {
            return _context.Students.Where(s => s.ClassId == classId).ToList();
        }

        public Student GetStudentByParentId(int parentId)
        {
            return _context.Students.FirstOrDefault(s => s.Parent.Id == parentId);
        }


    }
}
