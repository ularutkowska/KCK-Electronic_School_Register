using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System.Linq;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Controllers
{
    public class UserController
    {
        private readonly ApplicationDbContext _context;
        private Teacher loggedInTeacher;

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
                    return true;
                }
            }
            else if (role == "Parent")
            {
                var parent = _context.Parents.FirstOrDefault(p => p.Email == email && p.Password == password);
                return parent != null;
            }
            return false;
        }

        public bool StudentLogin(int studentId, string password)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == studentId && s.Password == password);
            return student != null;
        }

        public Teacher GetLoggedInTeacher()
        {
            return loggedInTeacher;
        }
    }
}
