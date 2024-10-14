using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateOnly Examday { get; set; }
    }
}
