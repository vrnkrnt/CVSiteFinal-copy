using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MessageService
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public ApplicationUser FindUser(string id)
        {
            ApplicationUser user = context.Users.Where(a => a.Id == id).FirstOrDefault();
            return user;

        }
        public List<MessageInfo> GetInbox(string id)
        {
            var inbox = context.GetMessageInfos.Where(m => m.ReceiverId == id).ToList();
            return inbox;
        }

        public Message FindMessages(int id)
        {
            var message = context.GetMessages.Where(m => m.MessageId == id).FirstOrDefault();
            return message;
        }

        public void Read(int id)
        {
            MessageInfo message = context.GetMessageInfos.Where(m => m.MessageId == id).FirstOrDefault();
            message.IsRead = true;
            context.SaveChanges();

        }

        public void SaveMessage(Message message) 
        {
            context.GetMessages.Add(message);
            context.SaveChanges();

        }

        public void SaveMessageUser(MessageInfo messageInfo)
        {
            context.GetMessageInfos.Add(messageInfo);
            context.SaveChanges();
        }

        public int NumberOfUnreadMessages(string id)
        {
            int unread = context.GetMessageInfos.Where(x => x.ReceiverId == id && x.IsRead == false).Count();
            return unread;
        }

    }
}

