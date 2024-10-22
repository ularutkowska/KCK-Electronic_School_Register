using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System.Linq;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Controllers
{
    public class UserController
    {
        private readonly ApplicationDbContext _context;
        private Teacher loggedInTeacher;
        private Parent loggedInParent;
        private Student loggedInStudent;
        private string loggedInRole;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Login(string email, string password, string role)
        {
            if (role == "Teacher")
            {
                var teacher = _context.Teachers.FirstOrDefault(t => t.Email == email && t.Password == password);
                if (teacher != null)
                {
                    loggedInTeacher = teacher;
                    loggedInRole = "Teacher";
                    return true;
                }
            }
            else if (role == "Parent")
            {
                var parent = _context.Parents.FirstOrDefault(p => p.Email == email && p.Password == password);
                if (parent != null)
                {
                    loggedInParent = parent;
                    loggedInRole = "Parent";
                    return true;
                }
            }
            return false;
        }

        public bool StudentLogin(int studentId, string password)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == studentId && s.Password == password);
            if (student != null)
            {
                loggedInStudent = student;
                loggedInRole = "Student";
                return true;
            }
            return false;
        }

        public string GetLoggedInRole()
        {
            return loggedInRole;
        }

        public Teacher GetLoggedInTeacher()
        {
            return loggedInTeacher;
        }

        public Parent GetLoggedInParent()
        {
            return loggedInParent;
        }

        public Student GetLoggedInStudent()
        {
            return loggedInStudent;
        }

        public object GetLoggedInUser()
        {
            if (loggedInRole == "Teacher") return loggedInTeacher;
            if (loggedInRole == "Parent") return loggedInParent;
            if (loggedInRole == "Student") return loggedInStudent;
            return null;
        }
    }
}
