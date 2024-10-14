using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Controllers
{
    public class UserController
    {
        private List<Teacher> teachers = new List<Teacher>();
        private List<Parent> parents = new List<Parent>();
        private List<Student> students = new List<Student>();
        public UserController()
        {
            teachers.Add(new Teacher { Id = 1, Name = "Ul", Surname = "Rut", Email = "rutkowska@school.com", Password = "teacher123" });

            parents.Add(new Parent { Id = 1, Name = "Ula", Surname = "Rutkowska", Email = "ula@gmail.com", Password = "parent123" });

            students.Add(new Student { Id = 1001, Name = "patryk", Surname = "piszczatowski", Password = "student123" });
        }

        public bool login(string email, string password, string role)
        {
            if (role == "Teacher")
            {
                var teacher = teachers.FirstOrDefault(t => t.Email == email && t.Password == password);
                return teacher != null;
            }
            else if (role == "Parent")
            {
                var parent = parents.FirstOrDefault(p => p.Email == email && p.Password == password);
                return parent != null;
            }
            return false;

        }
        public bool studentlogin(int studentid, string password)
        {
            var student = students.FirstOrDefault(s=>s.Id == studentid && s.Password == password);
            return student != null;
        }
    }
}