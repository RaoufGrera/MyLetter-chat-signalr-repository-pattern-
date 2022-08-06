using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyLetter.EF.Repository
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Notification Add(Questions questions,AppUser user);
        Notification Add(AppUser currentUser, AppUser user);

        void UpdateReadNotificationsMessageByUserId(int currentId ,int userId);
    }
}
