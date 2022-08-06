using MyLetter.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace MyLetter.EF.Repository
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

 

        public Notification Add(Questions questions, AppUser user)
        {

            Notification notification = new Notification
            {
                AppUserId = questions.SenderId,
                SourceId = user.Id,
                Username = user.UserName,
                SourceName = user.Name,
                Name = "answer",
                Content = "new_answer",
                IsRead = false,
                Url = $"/{user.Id}/#{questions.Id}"
            };
            _context.Notifications.Add(notification);
            return notification;

        }

        public Notification Add(AppUser currentUser, AppUser user)
        {
            Notification notification = new Notification
            {
                AppUserId = user.Id,
                SourceId = currentUser.Id,
                Username = user.UserName,
                SourceName = currentUser.Name,
                Name = "message",
                Content = "new_message",
                IsRead = false,
                Url = "/chat/" + currentUser.Id
            };
            _context.Notifications.Add(notification);
            return notification;
        }

        public void UpdateReadNotificationsMessageByUserId(int currentId , int userId)
        {
            _context.Notifications.Where(p => p.AppUserId == currentId && p.SourceId == userId && !p.IsRead && p.Name.Contains("message")).ToList().ForEach(e => e.IsRead = true);
        }
    }
}
