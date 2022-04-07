using ITech.Data;
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

        public NotificationsController(ApplicationDbContext context,
                                       UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public ViewResult Index()
        //{
        //    _userManager.GetUserId(HttpContext.User)
        //}
    }
}
