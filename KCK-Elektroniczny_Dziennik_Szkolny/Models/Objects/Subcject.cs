using System.ComponentModel.DataAnnotations;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Teacher Teacher { get; set; }
    }
}
