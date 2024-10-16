using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "The grade must be a number between 1 and 6.")]
        public int Value { get; set; }

        public Subject Subject { get; set; }

        public Teacher Teacher { get; set; }

        public Student Student { get; set; }

        public DateTime Date { get; set; }


    }
}
