using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Text { get; set; }

        public DateTime SentDate { get; set; }

        public Teacher? SenderTeacher { get; set; }

        public Student? SenderStudent { get; set; }

        public Parent? SenderParent { get; set; }

        public Teacher? ReceiverTeacher { get; set; }

        public Student? ReceiverStudent { get; set; }

        public Parent? ReceiverParent { get; set; }


    }
}
