using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shared.ViewModels
{
    public class CvViewModel
    {
        [Key]
        public string UserId { get; set; }

        [DisplayName("Competence")]
        [MaxLength(150, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Competence { get; set; }
 

        [DisplayName("Education")]
        [MaxLength(150, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Education { get; set; }

        [DisplayName("Experience")]
        [MaxLength(150, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Experience { get; set; }

        [DisplayName("Image")]
        //Ta bort??
        public string ImageUrl { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Cv Cv { get; set; }
        public List<Project> Projects { get; set; }

        public CvViewModel(string competence, string education, string experience, string image, string name, string email, string city, List<Project> projects, string fullname)
        {
            Competence = competence;
            Education = education;
            Experience = experience;
            ImageUrl = image;
            Username = name;
            Email = email;
            City = city;
            Projects = projects;
            Username = fullname;
        }

        public CvViewModel()
        {


        }


    }
    public class CvEditModel
    {
        [DisplayName("Competence")]
        [MaxLength(150, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Competence { get; set; }

        [DisplayName("Education")]
        [MaxLength(150, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Education { get; set; }

        [DisplayName("Experience")]
        [MaxLength(150, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Experience { get; set; }

        [DisplayName("Image")]
        public HttpPostedFileBase Image { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
    public class CvDeleteModel
    {

        public string Competence { get; set; }

        public string Education { get; set; }

        public string Experience { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
    public class CvSearchModel
    {
        public string Search { get; set; }
    }


}

