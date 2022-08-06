using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyLetter.EF.Models
{
    public class UserHobbies
    {
        [Key]
        public int Id { get; set; }
        public AppUser appUser { get; set; }
        public long AppUserId { get; set; }

        public Hobbies hobbies { get; set; }
        public string HobbiesId { get; set; }
    }
}
