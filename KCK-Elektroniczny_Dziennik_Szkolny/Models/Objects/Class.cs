using System.ComponentModel.DataAnnotations;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        public int Grade { get; set; }

        public ICollection<Student> Students { get; set; }

        public Teacher SupervisingTeacher { get; set; }
    }
}
