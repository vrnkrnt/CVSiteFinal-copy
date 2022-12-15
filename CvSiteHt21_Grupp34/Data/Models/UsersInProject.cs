using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UsersInProject
    {
        [Key, ForeignKey(nameof(User))]
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Key, ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }


}
