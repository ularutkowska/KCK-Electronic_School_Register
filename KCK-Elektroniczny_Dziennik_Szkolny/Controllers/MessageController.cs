using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System.Collections.Generic;
using System.Linq;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Controllers
{
    public class MessageController
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SendMessage(int senderId, string senderRole, int receiverId, string receiverRole, string subject, string content)
        {
            Message message = new Message{SenderId = senderId,SenderRole = senderRole,ReceiverId = receiverId,ReceiverRole = receiverRole,Subject = subject,Content = content,SentDate = DateTime.Now};

            _context.Messages.Add(message);
            _context.SaveChanges();
        }


        public List<Message> GetInbox(int userId, string userRole)
        {
            return _context.Messages
                           .Where(m => m.ReceiverId == userId && m.ReceiverRole == userRole)
                           .OrderByDescending(m => m.SentDate)
                           .ToList();
        }


        public List<Message> GetSentMessages(int userId)
        {
            return _context.Messages.Where(m => m.SenderId == userId).OrderByDescending(m => m.SentDate).ToList();
        }

        public void MarkAsRead(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message != null)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }
        }

        public string GetUserRole(int userId)
        {
            if (_context.Teachers.Any(t => t.Id == userId)) return "Teacher";
            if (_context.Students.Any(s => s.Id == userId)) return "Student";
            if (_context.Parents.Any(p => p.Id == userId)) return "Parent";

            return "Unknown";
        }

        public List<Teacher> GetTeachers()
        {
            return _context.Teachers.ToList();
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public List<Parent> GetParents()
        {
            return _context.Parents.ToList();
        }
        public Teacher GetTeacherById(int id)
        {
            return _context.Teachers.FirstOrDefault(t => t.Id == id);
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }

        public Parent GetParentById(int id)
        {
            return _context.Parents.FirstOrDefault(p => p.Id == id);
        }
    }
}
