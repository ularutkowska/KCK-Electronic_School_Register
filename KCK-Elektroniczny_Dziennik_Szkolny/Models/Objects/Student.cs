﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Student : IUser
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

        [Required]
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

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
