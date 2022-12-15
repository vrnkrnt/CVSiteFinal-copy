using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CvSiteHt21_Grupp34.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            ViewBag.ID = userID;

            using (var context = new ApplicationDbContext())
            {
                var userList = context.Users.ToList();
                return View(userList);
            }

        }
        //[HttpPost]
        public ActionResult Search(string name) 

        //Sökning på en användares namn som vi använder oss av för att kunna få fram CVs som är kopplade till namnet

        {
            string userID = User.Identity.GetUserId();
            ViewBag.ID = userID;

            using (var context = new ApplicationDbContext())
            {
                List<ApplicationUser> user = context.Users.Where(u => u.Fullname.Contains(name) || name == null).ToList();
                return View("Index", user);
            }

        }

    }
}