using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }
        [StringLength(250, ErrorMessage = "{0} can have a max of {1} characters", MinimumLength = 3)]
        public string Content { get; set; }
        public string Sender { get; set; }
        public string SenderId { get; set; }
        public string Receiver { get; set; }
        public string ReceiverId { get; set; }
        public bool IsRead { get; set; }
    }
}
