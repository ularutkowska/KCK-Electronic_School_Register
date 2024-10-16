using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "The grade must be a number between 1 and 6.")]
        public int Value { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
