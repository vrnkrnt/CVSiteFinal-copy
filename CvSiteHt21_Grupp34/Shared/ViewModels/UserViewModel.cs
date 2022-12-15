using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zåäöA-ZÅÄÖ\s]+$", ErrorMessage = "Use letters on fullname please")]
        [Display(Name = "Fullname")]
        public string Fullname { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zåäöA-ZÅÄÖ\s]+$", ErrorMessage = "Use letters on city please")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Private")]
        public bool IsPrivate { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public class IndexUserViewModel
        {
            
        }
    }
}
