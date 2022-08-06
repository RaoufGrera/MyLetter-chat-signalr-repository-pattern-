using Microsoft.AspNetCore.Identity;
using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetter.EF.Repository
{
    public interface IUserRepository : IBaseRepository<AppUser>
    {

        Task<IdentityResult> CreateUserAsync(AppUser appUser);
        Task<AppUser> GetUserByName(string userName);

        Task<AppUser> FindByEmailAsync(string email);

        Task<IList<string>> GetRolesAsync(AppUser user);

        AppUser EditProfileData(AppUser user,string table, string value);
    }
}
