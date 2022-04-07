using ITech.Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public NotificationRepository(ApplicationDbContext context,
                                        UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool Check(string notificationId)
        {
            var notification = _context.Notifications.Find(notificationId);
            if (notification == null)
                return false;
            notification.Checked = true;
            return _context.SaveChanges() > 0;
        }

        public async Task<List<Notification>> GetNotificationsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            return await _context.Notifications.Where(n => n.ReceiverId == userId).ToListAsync();
        }

        public async Task<List<Notification>> GetNotificationsAsync(AppUser user)
        {
            if (user == null)
                return null;
            return await _context.Notifications.Where(n => n.Receiver == user).ToListAsync();
        }

        public async Task<string> Notify(string senderId, string receiverId, string message)
        {
            var sender = await _userManager.FindByIdAsync(senderId);
            var receiver = await _userManager.FindByIdAsync(receiverId);
            if (sender == null || receiver == null || string.IsNullOrEmpty(message))
                return "";
            var notification = new Notification
            {
                Sender = sender,
                Receiver = receiver,
                Message = message
            };
            _context.Add(notification);

            return await _context.SaveChangesAsync() > 0 ? notification.Id : "";

        }

        public async Task<string[]> Notify(string senderId, List<AppUser> receivers, string message)
        {
            var sender = await _userManager.FindByIdAsync(senderId);
            if (sender == null || !receivers.Any() || string.IsNullOrEmpty(message))
                return Array.Empty<string>();
            var notification = new Notification
            {
                Sender = sender,
                Message = message
            };
            receivers.ForEach(r => r.Notifications.Add(notification));
            if (await _context.SaveChangesAsync() > 0)
            {
                string[] notificationIds = receivers.Select(r =>
                                            r.Notifications
                                               .FirstOrDefault(n => n.Id == notification.Id).Id)
                                            .ToArray();
                return notificationIds;
            }
            return Array.Empty<string>();
        }
        


    }
}
