using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Teacher : IUser
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "The name you entered is too long")]
        public string Name { get; set; }

        [MaxLength(30, ErrorMessage = "The surname you entered is too long")]
        public string Surname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 9, ErrorMessage = "The phone number must contain at least 9 digits.")]
        public string PhoneNumber { get; set; }

        public string GetDisplayName()
        {
            return $"{Name} {Surname}";
        }

        public int GetId()
        {
            return Id;
        }


    }
}
