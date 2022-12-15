using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Models;
using Shared.ViewModels;

namespace CvSiteHt21_Grupp34.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            ViewBag.ID = user;
            HomeViewModel model = new HomeViewModel();
            model.Projects = HämtaProjekt();
            model.Cvs = HämtaCvs();
            return View(model);
        }


        public List<Project> HämtaProjekt()
        {
            using (var context = new ApplicationDbContext())
            {
                var projects = context.GetProjects.ToList();
                return projects;
            }
        }

        public List<Cv> HämtaCvs()
        {
            using (var context = new ApplicationDbContext())
            {
                var cvs = context.GetCvs.ToList();
                return cvs;
            }
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }

}
