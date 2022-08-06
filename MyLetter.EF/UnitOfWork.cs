using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLetter.EF.Models;
using MyLetter.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyLetter.EF
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<AppUser> _mUserManager;

        public IUserRepository Users { get; private set; }

        public INotificationRepository Notifications { get; private set; }

        public IBaseRepository<Home> Homes { get; private set; }

        public IBaseRepository<Sector> Sectors { get; private set; }

        public IBaseRepository<Education> Educations { get; private set; }

        public IBaseRepository<Message> Messages { get; private set; }

        public IBaseRepository<GroupMessages> GroupMessages { get; private set; }

        public IBaseRepository<Questions> Questions => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _mUserManager = userManager;
            _context = context;
             Educations = new BaseRepository<Education>(_context);
            Messages = new BaseRepository<Message>(_context);
            GroupMessages = new BaseRepository<GroupMessages>(_context);
            Sectors = new BaseRepository<Sector>(_context);
            Users = new UserRepository(_context, _mUserManager);
            Notifications = new NotificationRepository(_context);

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
