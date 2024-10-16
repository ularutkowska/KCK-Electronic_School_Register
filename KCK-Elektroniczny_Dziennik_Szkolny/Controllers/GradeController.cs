using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
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
                .ToList();
        }
    }
}
