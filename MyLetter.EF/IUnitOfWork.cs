using MyLetter.EF.Models;
using MyLetter.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetter.EF
{
    public interface IUnitOfWork : IDisposable
    {
 

        IBaseRepository<Home> Homes { get; }
        IBaseRepository<GroupMessages> GroupMessages { get; }

        IBaseRepository<Sector> Sectors { get; }
        IBaseRepository<Education> Educations { get; }

        IBaseRepository<Message> Messages { get; }
        IBaseRepository<Questions> Questions { get; }


        IUserRepository Users { get; }
        INotificationRepository Notifications { get; }

        int Complete();
    }
}
