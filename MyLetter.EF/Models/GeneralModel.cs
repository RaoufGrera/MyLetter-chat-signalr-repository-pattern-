using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLetter.EF.Models;

namespace MyLetter.EF.Models
{
    public class GeneralModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }

        [Required, MaxLength(70)]
        public string NameEn { get; set; }

        public DateTime CreationDate = DateTime.UtcNow;
    }

    public class Seeking : GeneralModel { }
    public class Country : GeneralModel { }
    public class Home : GeneralModel { }
    public class Salary : GeneralModel { }
    public class Book : GeneralModel { }
    public class Age : GeneralModel { }
    public class Height : GeneralModel { }
    public class Gender : GeneralModel { }

    #region Essentials Models
    public class Sector : GeneralModel { }
    public class Education : GeneralModel { }
    public class Relationship : GeneralModel { }
    public class Havekids : GeneralModel { }

    public class Zodiac : GeneralModel { }
    public class Personality : GeneralModel { }
    #endregion

    #region Life Style Models
    public class FamilyValues : GeneralModel { }
    public class Relocate : GeneralModel { }
    public class PolygamyOpinion : GeneralModel { }
    public class Driver : GeneralModel { }
    public class Smoking : GeneralModel { }
    public class Work : GeneralModel { }
    public class WantKids : GeneralModel { }
    #endregion
    public class Hobbies : GeneralModel { public ICollection<UserHobbies> UserHobbies { get; set; } }

    public class Stamp : GeneralModel
    {
        public string Image { get; set; }
        public string Title { get; set; }
    }
}
