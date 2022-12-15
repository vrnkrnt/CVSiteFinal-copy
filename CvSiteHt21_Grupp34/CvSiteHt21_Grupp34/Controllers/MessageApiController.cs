using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CvSiteHt21_Grupp34.Controllers
{
    public class MessageApiController : ApiController
    {
        private MessageService service = new MessageService();

        [Route("api/SendMessage/{id}/{content}/{sender}")]
        [HttpGet]
        public IHttpActionResult SendMessage(string id, string content, string sender)
        {
            using (var context = new ApplicationDbContext())
            {
                var receiver = service.FindUser(id);

                var message = new Message()
                {
                    Content = content,
                    Date = DateTime.Now
                };

                if (receiver == null)
                {
                    return BadRequest();
                }
                else
                {
                    service.SaveMessage(message);

                    var messageInfo = new MessageInfo()
                    {
                        MessageId = message.MessageId,
                        Sender = sender,
                        ReceiverId = id,
                        IsRead = false
                    };
                    service.SaveMessageUser(messageInfo);
                    return Ok();
                }
            }
        }

        [HttpGet]
        [Route("api/NumberOfUnreadMessages/")]
        public int NumberOfUnreadMessages()
        {
            string userid = User.Identity.GetUserId();


            using (var context = new ApplicationDbContext())
            {
                ApplicationUser user = service.FindUser(userid);
                int unread = service.NumberOfUnreadMessages(userid);
                return unread;
            }
        }
    }
}