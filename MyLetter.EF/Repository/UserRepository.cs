using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyLetter.EF.Repository
{
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _mUserManager;

        public UserRepository(ApplicationDbContext context, UserManager<AppUser> userManager) : base(context)
        {
            _context = context;
            _mUserManager = userManager;
        }




        public Task<IdentityResult> CreateUserAsync(AppUser appUser)
        {
            return _mUserManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));
        }

        public AppUser EditProfileData(AppUser appUser, string table,string value)
        {
            switch (table)
            {
                case "hobbies":

                    string[] words = value.Split(",");
                    var list = _context.UserHobbies.Where(c => c.AppUserId == appUser.Id).ToList();
                    _context.UserHobbies.RemoveRange(list);
                    
                    if (words.Length == 0)
                        break;
                    var items = _context.Hobbies.Where(p => words.Contains(p.Id)).Select(p => p.Id).ToList();
                    if (!items.Any())
                        break;

                    foreach (var item in items)
                    {
                        UserHobbies userHobbies = new UserHobbies();
                        userHobbies.HobbiesId = item;
                        userHobbies.AppUserId = appUser.Id;
                        _context.UserHobbies.Add(userHobbies);
                    }
                    


                    break;
                //social_status
                case "gender":
                    appUser.GenderId = _context.Gender.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    //   var folderName = Path.Combine("StaticFiles", "Images");
                    var maleImagePath = "default-male.svg";
                    var femaleImagePath = "default-female.svg";

                    if (maleImagePath == appUser.Image)
                        appUser.Image = femaleImagePath;


                    
                    break;
                //case "seeking":
                //    appUser.SeekingId = _context.Seeking.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                //    
                //break;
                case "home":
                    appUser.HomeId = _context.Home.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;

                case "personality":
                    appUser.PersonalityId = _context.Personality.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "country":
                    appUser.CountryId = _context.Country.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "age":
                    appUser.AgeId = _context.Age.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "height":
                    appUser.HeightId = _context.Height.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "sector":

                    appUser.SectorId = _context.Sector.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;

                case "book":

                    appUser.BookId = _context.Book.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "education":
                    appUser.EducationId = _context.Education.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "relationship":
                    appUser.RelationshipId = _context.Relationship.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                //case "have_kids":
                //    appUser.HavekidsId = _context.Havekids.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                //    
                //break;
                case "zodiac":
                    appUser.ZodiacId = _context.Zodiac.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;

                //lifestyle
                case "family_values":
                    appUser.FamilyValuesId = _context.FamilyValues.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                //case "relocate":
                //    appUser.RelocateId = _context.Relocate.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                //    
                //    break;
                //case "polygamy_opinion":
                //    appUser.PolygamyOpinionId = _context.PolygamyOpinion.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                //    
                //    break;
                case "driver":
                    appUser.DriverId = _context.Driver.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "work":
                    appUser.WorkId = _context.Work.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "smoking":
                    appUser.SmokingId = _context.Smoking.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                case "salary":
                    appUser.SalaryId = _context.Salary.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                    
                    break;
                //case "want_kids":
                //    appUser.WantKidsId = _context.WantKids.Where(o => o.Id == value).Select(p => (p.Id != null) ? p.Id : null).SingleOrDefault();
                //    
                //    break;
                case "name":
                    appUser.Name = value;
                    
                    break;

                case "username":
                    if (value != null && value.Length > 3)
                    {
                        var isexist = _context.Users.Where(u => u.UserName == value).Any();
                        if (!isexist)
                        {
                            appUser.UserName = value;
                        }
                    }
                    break;
                case "about":
                    appUser.AboutMe = value;
                    
                    break;

                default:
                    break;
                    //case "looking":
                    //    appUser.SearchFor = value;
                    //    
                    //    break;

            }

            return appUser;
        }

        public Task<AppUser> FindByEmailAsync(string email)
        {
            return _mUserManager.FindByEmailAsync(email);
        }

        public Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return _mUserManager.GetRolesAsync(user);
        }

        public Task<AppUser> GetUserByName(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }

    }

}

