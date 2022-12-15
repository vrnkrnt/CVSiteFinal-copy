using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [DisplayName("Title")]
        [MaxLength(50, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Title { get; set; }

        [DisplayName("Description")]
        [MaxLength(250, ErrorMessage = "{0} can have a max of {1} characters")]
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string Creator { get; set; }
        public virtual ApplicationUser User { get; set; }
        public List<Project> Projects { get; set; }
    }
}
