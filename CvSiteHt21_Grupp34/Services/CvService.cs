using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Shared.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Services
{
    public class CvService
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private readonly HttpContext _httpcontext;

        public CvService(HttpContext httpcontext)
        {
            _httpcontext = httpcontext;
        }

        public void CreateNewCv(CvViewModel model)

        //Service som används genom att en användare ska kunna skapa ett nytt CV

        {
            using (var context = new ApplicationDbContext())
            {
                var newCV = new Cv();
                {
                    string currentUserId = _httpcontext.User.Identity.GetUserId();
                    ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
                    newCV.User = currentUser;
                    newCV.Competence = model.Competence;
                    newCV.Experience = model.Experience;
                    newCV.Education = model.Education;
                };
                if (model.Image == null) //Lägger till en defaultbild om användaren valt att inte ladda upp en egen bild på sitt CV
                {
                    var filepath = _httpcontext.Server.MapPath("~/Images");
                    var filename = Path.GetFileName("~/Images/DefaultImage.jpg");
                    newCV.ImageUrl = filename;
                }
                else if  (model.Image != null)
                {
                    var filename = Path.GetFileName(model.Image.FileName);
                    var filepath = _httpcontext.Server.MapPath("~/Images");
                    model.Image.SaveAs(filepath + "/" + filename);
                    newCV.ImageUrl = filename;
                }
                context.GetCvs.Add(newCV); //Lägger till det nya CVt i CVlistan
                context.SaveChanges();
            }
        }
    }
}
