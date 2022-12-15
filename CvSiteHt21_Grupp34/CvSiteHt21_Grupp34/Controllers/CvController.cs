using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;
using Shared.ViewModels;

namespace CvSiteHt21_Grupp34.Controllers
{
    public class CvController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CvService cvService = new CvService(System.Web.HttpContext.Current);

        // GET: Cv
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            ViewBag.ID = user;

            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var Cvs = context.GetCvs.ToList();
            return View(Cvs);
        }

        // GET: Cv/Details/5
        public ActionResult Details(string id)
        {
            var context = new ApplicationDbContext();

            Cv cv = context.GetCvs.Find(id);
            ApplicationUser user = context.Users.Find(cv.UserId);

            var files = Directory.GetFiles(Server.MapPath("~/Images/"));
            var cvImage = cv.ImageUrl;
            ViewBag.Path = ("~/Images/") + cvImage;

            var experience = cv.Experience;
            var education = cv.Education;
            var competence = cv.Competence;
            var image = "/Images/" + cv.ImageUrl;
            var name = user.UserName;
            var email = user.Email;
            var city = user.City;
            var username = user.Fullname;
            ViewBag.ID = user.Id;

            List<Project> project = context.GetProjects.ToList();
            List<UsersInProject> projectWithUsers = context.GetUsersInProjects.Where(x => x.ApplicationUserID == id).ToList();

            List<Project> projects = new List<Project>();
            foreach (var a in project)
                foreach (var b in projectWithUsers)
                {
                    if (a.ProjectId == b.ProjectId)
                    {
                        projects.Add(a);
                    }

                }
            CvViewModel CvView = new CvViewModel(competence, education, experience, image, name, email, city, projects, username);

            return View(CvView);
        }
    

        // GET: Cv/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            return View();
        }

        // POST: Cv/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CvViewModel model)
        {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            try
            {
                if (ModelState.IsValid)
                {
                    var service = new CvService(System.Web.HttpContext.Current);

                    service.CreateNewCv(model);

                }
                return RedirectToAction("Index");
            }

            catch
            {

                return RedirectToAction("Index");

            }
        }

        // GET: Cv/Edit/5
        public ActionResult Edit(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cv cv = context.GetCvs.Find(id);

                return View(cv);

            }

        }

        // POST: Cv/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Competence,Education,Experience,ImageUrl")] Cv cv)
        {
            using (var context = new ApplicationDbContext())

            {
                if (ModelState.IsValid)
                {

                    context.Entry(cv).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Cv/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cv cv = db.GetCvs.Find(id);
            if (cv == null)
            {
                return HttpNotFound();
            }
            return View(cv);
        }

        // POST: Cv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Cv cv = db.GetCvs.Find(id);
            db.GetCvs.Remove(cv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
