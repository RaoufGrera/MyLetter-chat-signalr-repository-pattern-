using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLetter.EF.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public Stamp Stamp { get; set; }
        public string StampId { get; set; }
        public string CountChar { get; set; }
        public string Secret { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public AppUser Sender { get; set; }
        public GroupMessages GroupMessages { get; set; }
        public long GroupMessageId { get; set; }

          public int RecipientId { get; set; }
          public string RecipientUsername { get; set; }
        public AppUser Recipient { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow.AddHours(2);
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
        public bool IsPublic { get; set; } = false;
        public PublicMessage PublicMessage { get; set; }
        public string PublicMessageId { get; set; }
        public int ShowCount { get; set; }
        public int LikeCount { get; set; }

        public DateTime ArrivalDate { get; set; }

    }
}