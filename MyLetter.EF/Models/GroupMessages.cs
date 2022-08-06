using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyLetter.EF.Models
{
    public class GroupMessages
    {
        [Key]
        public long Id { get; set; }

        public AppUser Sender { get; set; }
        public long SenderId { get; set; }

        public AppUser Recipient { get; set; }
        public long RecipientId { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
