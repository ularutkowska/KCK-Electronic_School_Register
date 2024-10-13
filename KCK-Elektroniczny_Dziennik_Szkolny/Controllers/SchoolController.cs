using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Controllers
{
    public class SchoolController
    {
        private List<Class> classes = new List<Class>();
        private List<Student> students = new List<Student>();
        private List<Teacher> teachers = new List<Teacher>();

        public void AddClass(Class schoolClass)
        {
            classes.Add(schoolClass);
        }
        public void AddStudent(Student student)
        {
            students.Add(student);
        }
        public void AddTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
        }

        public List<Class> GetClasses()
        {
            return classes;
        }
        public List<Student> GetStudents()
        {
            return students;
        }
        public List<Teacher> GetTeacher()
        {
            return teachers;
        }
    }
}
