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
        public int Id { get; set; }

        public int SenderId { get; set; }

        public string SenderRole { get; set; }

        public int ReceiverId { get; set; }

        public string ReceiverRole { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime SentDate { get; set; }

        public bool IsRead { get; set; }
    }
}
