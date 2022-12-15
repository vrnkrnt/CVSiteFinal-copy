using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Data;
using Shared.ViewModels;



namespace Services
{
    public class UserService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly HttpContext _httpcontext;

        public UserService(HttpContext httpcontext)
        {
            _httpcontext = httpcontext;
        }
        public UserViewModel GetUserIndexView(string id)

        //Service som tar fram information om en användare kopplat till ett ID

        {
            ApplicationUser user = db.Users.Find(id);
            var newUserView = new UserViewModel
            {
                Id = user.Id,
                Fullname = user.Fullname,
                City = user.City,
                Email = user.Email
            };
            return newUserView;
        }
    }
}
