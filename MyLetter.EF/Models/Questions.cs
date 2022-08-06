﻿using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetter.EF.Models
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public long SenderId { get; set; }
        public AppUser Sender { get; set; }
        public long RecipientId { get; set; }
        public AppUser Recipient { get; set; }
        public string Ask { get; set; }


        public string Answer { get; set; }

        public DateTime? DateRead { get; set; }
        public DateTime QuestoinSent { get; set; } = DateTime.UtcNow;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }


        public Stamp Stamp { get; set; }
        public string StampId { get; set; }
    }
}
