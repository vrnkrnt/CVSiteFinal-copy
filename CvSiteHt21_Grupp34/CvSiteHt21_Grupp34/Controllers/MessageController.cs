using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Services;
using Shared.ViewModels;

namespace CvSiteHt21_Grupp34.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly MessageService service = new MessageService();

        // GET: Message
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            ViewBag.ID = user;

            var inbox = service.GetInbox(user);

            var listWithMessages = new List<MessageViewModel>();
            foreach (var msg in inbox)
            {
                var ViewModel = new MessageViewModel
                {
                    ReceiverId = user,
                    Sender = msg.Sender,
                    MessageId = msg.MessageId,
                    IsRead = msg.IsRead
                };
                var message = service.FindMessages(msg.MessageId);
                ViewModel.Content = message.Content;

                listWithMessages.Add(ViewModel);
            }

            return View(listWithMessages);
        }

        public ActionResult MarkAsRead(int id) //Tillåter en användare att markera ett meddelande som läst
        {
            service.Read(id);
            return RedirectToAction("Index");
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var message = db.GetMessages.Find(id);
            if (message == null) return HttpNotFound();
            return View(message);
        }

        // GET: Message/Create
        public ActionResult Create(string id) //Tillåter en en användare att skicka ett meddelande till en annan användare
        {
            try
            {
                var senderId = User.Identity.GetUserId(); 
                var sender = db.Users.Find(senderId);
                var receiver = db.Users.Find(id);

                var receiverId = receiver.Id;
                var receiverName = receiver.Fullname;

                if (sender == null)
                {
                    var anonymous = new MessageInfo();
                    var anonymousMessage = anonymous.Sender = "Anonymous";
                    var anonymousViewModel = new MessageViewModel
                    {
                        Sender = anonymous.Sender,
                        ReceiverId = receiverId,
                        Receiver = receiverName
                    };
                    return View(anonymousViewModel);
                }

                var ViewModel = new MessageViewModel
                {
                    Sender = sender.Fullname,
                    ReceiverId = receiverId,
                    Receiver = receiverName
                };
                return View(ViewModel);
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var message = db.GetMessages.Find(id);
            if (message == null) return HttpNotFound();
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageId,Content,Date")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var message = db.GetMessages.Find(id);
            if (message == null) return HttpNotFound();
            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var message = db.GetMessages.Find(id);
            db.GetMessages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}