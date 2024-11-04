using KCK_Elektroniczny_Dziennik_Szkolny.Controllers;
using KCK_Elektroniczny_Dziennik_Szkolny.Models;
using KCK_Elektroniczny_Dziennik_Szkolny.Models.Objects;
using System;
using System.Collections.Generic;

namespace KCK_Elektroniczny_Dziennik_Szkolny.Views
{
    public class MessageView
    {
        private MessageController messageController;
        private int loggedInUserId;
        private UserController userController;
        public MessageView(MessageController messageController, UserController userController, int userId)
        {
            this.messageController = messageController;
            this.userController = userController;
            loggedInUserId = userId;
        }

        public void DisplayInbox()
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("Inbox") + "\n");

            string userRole = userController.GetLoggedInRole();

            List<Message> inbox = messageController.GetInbox(loggedInUserId, userRole);

            if (inbox.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("NoMessages"));
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < inbox.Count; i++)
            {
                var message = inbox[i];
                string senderName = GetUserNameById(message.SenderId, message.SenderRole);
                Console.WriteLine($"{i + 1}. {(message.IsRead ? LanguageManager.GetString("Message_Read") : LanguageManager.GetString("Message_Unread"))} {LanguageManager.GetString("From")}: {senderName}, {LanguageManager.GetString("Subject1")}: {message.Subject}, {LanguageManager.GetString("Sent")}: {message.SentDate}");
            }

            Console.WriteLine(LanguageManager.GetString("Select_Message_To_Read"));
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
            Console.WriteLine($"{LanguageManager.GetString("From")}: {senderName}\n" +
                              $"{LanguageManager.GetString("Subject1")}: {message.Subject}\n" +
                              $"{LanguageManager.GetString("Sent")}: {message.SentDate.ToShortDateString()}\n\n" +
                              $"{message.Content}");
            messageController.MarkAsRead(message.Id);
            Console.ReadKey();
            DisplayInbox();
        }

        public void DisplaySentMessages()
        {
            Console.Clear();
            Console.WriteLine(LanguageManager.GetString("SentMessages") + "\n");

            string userRole = userController.GetLoggedInRole();
            List<Message> sentMessages = messageController.GetSentMessagesByUser(loggedInUserId, userRole);

            if (sentMessages.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("NoSentMessages"));
                return;
            }

            for (int i = 0; i < sentMessages.Count; i++)
            {
                var message = sentMessages[i];
                string receiverName = GetUserNameById(message.ReceiverId, message.ReceiverRole);
                Console.WriteLine($"{i + 1}. {LanguageManager.GetString("To")}: {receiverName}, {LanguageManager.GetString("Subject1")}: {message.Subject}, {LanguageManager.GetString("Sent")}: {message.SentDate}");
            }

            Console.WriteLine("\n" + LanguageManager.GetString("SelectMessageDetails"));
            int selectedMessage;
            if (int.TryParse(Console.ReadLine(), out selectedMessage) && selectedMessage > 0 && selectedMessage <= sentMessages.Count)
            {
                DisplaySentMessageDetails(sentMessages[selectedMessage - 1]);
            }
        }


        private void DisplaySentMessageDetails(Message message)
        {
            Console.Clear();
            string receiverName = GetUserNameById(message.ReceiverId, message.ReceiverRole);
            Console.WriteLine($"{LanguageManager.GetString("To")}: {receiverName}");
            Console.WriteLine($"{LanguageManager.GetString("Subject")}: {message.Subject}");
            Console.WriteLine($"{LanguageManager.GetString("Sent")}: {message.SentDate}");
            Console.WriteLine("\n" + LanguageManager.GetString("MessageContent") + ":\n");
            Console.WriteLine(message.Content);
            Console.WriteLine("\n" + LanguageManager.GetString("PressAnyKeyToGoBack") + ".");
            Console.ReadKey();
            DisplaySentMessages();
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
            Console.WriteLine(LanguageManager.GetString("ComposeMessage") + "\n");

            Console.WriteLine(LanguageManager.GetString("SelectRecipientType"));
            Console.WriteLine("1. " + LanguageManager.GetString("Teacher"));
            Console.WriteLine("2. " + LanguageManager.GetString("Student"));
            Console.WriteLine("3. " + LanguageManager.GetString("Parent"));


            int recipientType = 0;
            while (recipientType < 1 || recipientType > 3)
            {
                Console.WriteLine(LanguageManager.GetString("EnterValidOption"));
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
                Console.WriteLine(LanguageManager.GetString("InvalidRecipientSelected") + " " + LanguageManager.GetString("AbortingMessage"));
                Console.ReadKey();
                return;
            }

            Console.Write(LanguageManager.GetString("EnterSubject") + ": ");
            string subject = Console.ReadLine();

            Console.Write(LanguageManager.GetString("EnterContent") + ": ");
            string content = Console.ReadLine();

            string senderRole = userController.GetLoggedInRole();

            messageController.SendMessage(loggedInUserId, senderRole, receiverId, receiverRole, subject, content);

            Console.WriteLine(LanguageManager.GetString("MessageSent"));
            Console.ReadKey();
        }




        private int SelectRecipientFromList<T>(List<T> recipients) where T : IUser
        {
            if (recipients.Count == 0)
            {
                Console.WriteLine(LanguageManager.GetString("NoRecipientsFound"));
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
                Console.WriteLine(LanguageManager.GetString("SelectRecipient") + ":");
                int.TryParse(Console.ReadLine(), out selectedRecipient);
            }

            return recipients[selectedRecipient - 1].GetId();
        }
    }
}
