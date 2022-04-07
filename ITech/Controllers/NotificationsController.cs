using ITech.Data;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationRepository _notificationRepository;

        public NotificationsController(ApplicationDbContext context,
                                       UserManager<AppUser> userManager,
                                       INotificationRepository  notificationRepository)
        {
            _context = context;
            _userManager = userManager;
            _notificationRepository = notificationRepository;
        }

        public async  Task<IActionResult> Index()
        {
            var user = await  _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            return View(await _notificationRepository.GetNotificationsAsync(user));
        }
    }
}
