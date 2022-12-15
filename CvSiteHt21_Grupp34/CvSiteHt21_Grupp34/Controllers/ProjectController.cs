using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Project
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                string user = User.Identity.GetUserId();
                ViewBag.ID = user;
                var service = new ProjectService(System.Web.HttpContext.Current);

                var projects = context.GetProjects.ToList();
                return View(projects);
            }
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            {
                using (var context = new ApplicationDbContext())
                {
                    string user = User.Identity.GetUserId();
                    ViewBag.ID = user;
                    Project project = context.GetProjects.Find(id);
                    return View(project);
                }
            }
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel model)
        {
            try
            {
                var service = new ProjectService(System.Web.HttpContext.Current);
                service.CreateNewProject(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Project project = context.GetProjects.Find(id);

                return View(project);
            }
            catch
            {
                return View();
            }
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Title,Description,Date,CreatorId")] Project project)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    if (ModelState.IsValid)
                    {
                        context.Entry(project).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Project project = context.GetProjects.Find(id);
                if (project == null)
                {
                    return HttpNotFound();
                }
                return View(project);
            }
            catch
            {
                return View();
            }
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    string user = User.Identity.GetUserId();

                    Project project = context.GetProjects.Where(p => p.ProjectId == id).FirstOrDefault();

                    UsersInProject usersInProject = context.GetUsersInProjects.
                        Where(p => p.ProjectId == id && p.ApplicationUserID == user)
                        .FirstOrDefault();
                    if (usersInProject == null)
                    {
                        context.GetProjects.Remove(project);
                    }
                    else
                    {
                        context.GetUsersInProjects.Remove(usersInProject);
                        context.GetProjects.Remove(project);
                    }

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Get: Project/Join/5
        [Authorize]
        public ActionResult Join(int id, FormCollection collection)
        {
            try
            {
                var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                string user = User.Identity.GetUserId();
                ViewBag.ID = user;

                var newUsersInProject = new UsersInProject()
                {
                    ApplicationUserID = user,
                    ProjectId = id
                };

                context.GetUsersInProjects.Add(newUsersInProject);
                context.SaveChanges();

                if (user != null)
                {
                    var service = new ProjectService(System.Web.HttpContext.Current);

                    var userInProjectList = service.GetUsersProjects(user);
                    List<string> proj = new List<string>();

                    foreach (var p in userInProjectList)
                    {
                        proj.Add(p.ProjectId.ToString());
                    }


                    ViewBag.IsInProject = proj;
                }

                Project project = context.GetProjects.Find(id);

                return View("Details", project);


            }
            catch
            {
                var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                Project project = context.GetProjects.Find(id);
                return View("Details", project);
            }
        }
    }
}
