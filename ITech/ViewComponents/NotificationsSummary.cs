using ITech.Data;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.ViewComponents
{
    public class NotificationsSummary : ViewComponent
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly UserManager<AppUser> _userManager;

        public NotificationsSummary(INotificationRepository notificationRepository,
                                    UserManager<AppUser> userManager )
        {
            _notificationRepository = notificationRepository;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await _notificationRepository.GetNotificationsAsync(
                                                                _userManager.GetUserId(HttpContext.User));
            return View(notifications.Count(n => !n.Checked));
        }
    }
}
