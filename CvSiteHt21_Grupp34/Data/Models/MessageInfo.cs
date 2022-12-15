using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class MessageInfo
    {
        [Key]
        public string ReceiverId { get; set; }
        [Key]
        public int MessageId { get; set; }
        public string Sender { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Message Message { get; set; }
        public bool IsRead { get; set; }
    }
}
