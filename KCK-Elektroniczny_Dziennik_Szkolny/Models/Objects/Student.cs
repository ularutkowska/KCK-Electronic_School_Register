using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "The name you entered is too long")]
        public string Name { get; set; }

        [MaxLength(30, ErrorMessage = "The surname you entered is too long")]
        public string Surname { get; set; }

        [Required]
        public string Password { get; set; }

        public DateOnly BirthDate { get; set; }

        public Parent Parent { get; set; }

    }
}
