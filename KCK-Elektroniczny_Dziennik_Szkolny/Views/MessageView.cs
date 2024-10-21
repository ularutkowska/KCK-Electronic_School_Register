using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System;
using System.Collections.Generic;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class MessageView
    {
        private MessageController messageController;
        private int loggedInUserId;

        public MessageView(MessageController controller, int userId)
        {
            messageController = controller;
            loggedInUserId = userId;
        }

        public void DisplayInbox()
        {
            Console.Clear();
            Console.WriteLine("Inbox\n");

            string userRole = messageController.GetUserRole(loggedInUserId);

            List<Message> inbox = messageController.GetInbox(loggedInUserId, userRole);

            if (inbox.Count == 0)
            {
                Console.WriteLine("No messages.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < inbox.Count; i++)
            {
                var message = inbox[i];
                string senderName = GetUserNameById(message.SenderId, message.SenderRole);
                Console.WriteLine($"{i + 1}. {(message.IsRead ? "[Read]" : "[Unread]")} From: {senderName}, Subject: {message.Subject}, Sent: {message.SentDate}");
            }

            Console.WriteLine("Select a message to read (Enter number):");
            int selectedMessage;
            if (int.TryParse(Console.ReadLine(), out selectedMessage) && selectedMessage > 0 && selectedMessage <= inbox.Count)
            {
                DisplayMessageDetails(inbox[selectedMessage - 1]);
            }
        }

        public void DisplayMessageDetails(Message message)
        {
            Console.Clear();
            string senderName = GetUserNameById(message.SenderId, message.SenderRole);
            Console.WriteLine($"From: {senderName}\nSubject: {message.Subject}\nSent: {message.SentDate}\n\n{message.Content}");

            messageController.MarkAsRead(message.Id);
            Console.ReadKey();
        }

        public void DisplaySentMessages()
        {
            Console.Clear();
            Console.WriteLine("Sent Messages\n");

            List<Message> sentMessages = messageController.GetSentMessages(loggedInUserId);

            if (sentMessages.Count == 0)
            {
                Console.WriteLine("No sent messages.");
                return;
            }

            foreach (var message in sentMessages)
            {
                string receiverName = GetUserNameById(message.ReceiverId, message.ReceiverRole);
                Console.WriteLine($"To: {receiverName}, Subject: {message.Subject}, Sent: {message.SentDate}");
            }

            Console.ReadKey();
        }



        private string GetUserNameById(int userId, string role)
        {
            switch (role)
            {
                case "Teacher":
                    var teacher = messageController.GetTeacherById(userId);
                    if (teacher != null) return $"{teacher.Name} {teacher.Surname}";
                    break;

                case "Student":
                    var student = messageController.GetStudentById(userId);
                    if (student != null) return $"{student.Name} {student.Surname}";
                    break;

                case "Parent":
                    var parent = messageController.GetParentById(userId);
                    if (parent != null) return $"{parent.Name} {parent.Surname}";
                    break;
            }

            return "Unknown User";
        }

        private string GetUserRole(int userId)
        {
            var teacher = messageController.GetTeacherById(userId);
            if (teacher != null)
            {
                return "Teacher";
            }

            var student = messageController.GetStudentById(userId);
            if (student != null)
            {
                return "Student";
            }

            var parent = messageController.GetParentById(userId);
            if (parent != null)
            {
                return "Parent";
            }

            return "Unknown";
        }

        public void ComposeMessage()
        {
            Console.Clear();
            Console.WriteLine("Compose Message\n");

            Console.WriteLine("Select the recipient type:");
            Console.WriteLine("1. Teacher");
            Console.WriteLine("2. Student");
            Console.WriteLine("3. Parent");

            int recipientType = 0;
            while (recipientType < 1 || recipientType > 3)
            {
                Console.WriteLine("Enter a valid option (1-3):");
                int.TryParse(Console.ReadLine(), out recipientType);
            }

            int receiverId = 0;
            string receiverRole = "";

            switch (recipientType)
            {
                case 1:
                    var teachers = messageController.GetTeachers();
                    receiverId = SelectRecipientFromList(teachers);
                    receiverRole = "Teacher";
                    break;

                case 2:
                    var students = messageController.GetStudents();
                    receiverId = SelectRecipientFromList(students);
                    receiverRole = "Student";
                    break;

                case 3:
                    var parents = messageController.GetParents();
                    receiverId = SelectRecipientFromList(parents);
                    receiverRole = "Parent";
                    break;
            }

            if (receiverId == 0)
            {
                Console.WriteLine("Invalid recipient selected. Aborting message.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();

            Console.Write("Enter content: ");
            string content = Console.ReadLine();

            string senderRole = GetUserRole(loggedInUserId);

            messageController.SendMessage(loggedInUserId, senderRole, receiverId, receiverRole, subject, content);

            Console.WriteLine("Message sent.");
            Console.ReadKey();
        }




        private int SelectRecipientFromList<T>(List<T> recipients) where T : IUser
        {
            if (recipients.Count == 0)
            {
                Console.WriteLine("No recipients found.");
                Console.ReadKey();
                return 0;
            }

            for (int i = 0; i < recipients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipients[i].GetDisplayName()}");
            }

            int selectedRecipient = 0;
            while (selectedRecipient < 1 || selectedRecipient > recipients.Count)
            {
                Console.WriteLine("Select a recipient (Enter number):");
                int.TryParse(Console.ReadLine(), out selectedRecipient);
            }

            return recipients[selectedRecipient - 1].GetId();
        }
    }
}
