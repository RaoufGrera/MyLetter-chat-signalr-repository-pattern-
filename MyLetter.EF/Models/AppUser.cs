using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MyLetter.EF.Models
{

    public class AppRole : IdentityRole<int> { }

    public class AppUser : IdentityUser<int>
    {
    
        public bool Online { get; set; }

        public int ShowCount { get; set; }
        public int LikeCount { get; set; }

        public long? FacebookId { get; set; }
        public string GmailId { get; set; }
        public string AboutMe { get; set; }
 
        public string Name { get; set; }
        public string FcmToken { get; set; }

        public virtual Home Home { get; set; }
        public string HomeId { get; set; }

        public virtual Country Country { get; set; }
        public string CountryId { get; set; }

        public virtual Salary Salary { get; set; }
        public string SalaryId { get; set; }
        public virtual Gender Gender { get; set; }
        public string GenderId { get; set; }
        public virtual Age Age { get; set; }
        public string AgeId { get; set; }

        public virtual Height Height { get; set; }
        public string HeightId { get; set; }

        public virtual Sector Sector { get; set; }
        public string SectorId { get; set; }
        public virtual Book Book { get; set; }
        public string BookId { get; set; }
        public virtual Education Education { get; set; }
        public string EducationId { get; set; }

        public virtual Relationship Relationship { get; set; }
        public string RelationshipId { get; set; }


        public virtual Zodiac Zodiac { get; set; }
        public string ZodiacId { get; set; }



        public virtual FamilyValues FamilyValues { get; set; }
        public string FamilyValuesId { get; set; }

        public virtual Smoking Smoking { get; set; }
        public string SmokingId { get; set; }
        public virtual Driver Driver { get; set; }
        public string DriverId { get; set; }
        public virtual Personality Personality { get; set; }
        public string PersonalityId { get; set; }

        public virtual Work Work { get; set; }
        public string WorkId { get; set; }

        public string Provider { get; set; }

        public string Image { get; set; }


        public string Token { get; set; }


        public string Password { get; set; }

        public DateTime Created { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime LastActive { get; set; } = DateTime.Now.ToUniversalTime();


        public ICollection<Notification> NotificationsUsers { get; set; }
        public ICollection<Notification> NotificationsSourceUsers { get; set; }

        public ICollection<PublicMessage> publicMessagesUser { get; set; }

        public ICollection<GroupMessages> GroupMessagesSender { get; set; }
        public ICollection<GroupMessages> GroupMessagesRecipient { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

    }

}
