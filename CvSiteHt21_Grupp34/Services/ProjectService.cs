using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Shared.ViewModels;

namespace Services
{
    public class ProjectService
    {
        private readonly HttpContext _httpcontext;
        private ApplicationDbContext _context { get; set; }

        public ProjectService(HttpContext httpcontext)
        {
            _httpcontext = httpcontext;
            _context = new ApplicationDbContext();
        }

        public void CreateNewProject(ProjectViewModel model)
        {

        //Service som gör att användare kan skapa projekt

            using (var context = new ApplicationDbContext())
            {
                string ID = _httpcontext.User.Identity.GetUserId();

                var newProject = new Project()
                {
                    CreatorId = ID,
                    Title = model.Title,
                    Description = model.Description,
                    Date = model.Date
                };

                context.GetProjects.Add(newProject); //Här läggs det nya projektet till i projektlistan
                context.SaveChanges();

            }
        }

        public void Delete(int id)
        {

        }
        public Project GetProject(int id)

        //Service som används för att iterera genom projektlistan för att hitta ett projekt med ett specifikt projektID

        {
            Project proj = _context.GetProjects.Where(x => x.ProjectId == id).FirstOrDefault();
            return proj;
        }



        public UsersInProject GetProjectsAndUsers(string userid, int projectid)
        {
            return _context.GetUsersInProjects.Where(x => x.ApplicationUserID == userid && x.ProjectId == projectid).FirstOrDefault();

        }

        public List<UsersInProject> GetUsersProjects(string id)
        {
            return _context.GetUsersInProjects.Where(x => x.ApplicationUserID == id).ToList(); ;
        }
    }
}

