using ITech.Data;
using ITech.Data.Repositories;
using ITech.Models;
using ITech.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationRepository _notificationRepository;

        public HomeController(ILogger<HomeController> logger,
                                IProductRepository productRepository,
                                ICategoryRepository categoryRepository,
                                ApplicationDbContext context,
                                UserManager<AppUser> userManager,
                                INotificationRepository notificationRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
            _userManager = userManager;
            _notificationRepository = notificationRepository;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                AllCategories = _categoryRepository.GetAllCategories(),
                TopSellingProducts = _productRepository.GetTopSellingProducts(10, true)
                                        .Where(psa => psa.SoldAmount > 0).ToArray()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> ApplyForJob(string id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var application = _context.JobApplications.
                FirstOrDefault(ja => ja.ApplicantId == userId && ja.JobId == id);
            if (application == null)
            {
                application = new JobApplication
                {
                    ApplicantId = userId,
                    JobId = id,
                };
                _context.Add(application);
                _context.SaveChanges();
                var admin = await _userManager.FindByNameAsync("admin");
                var message = "Successfully Applied your application: " + application.Id + ". You will get response soon!";
                await _notificationRepository.NotifyAsync(admin.Id, userId, message);
            }
            TempData["StatusMessage"] = "Job Applied, Application ID: " + application.Id;

            return RedirectToAction(nameof(Jobs));
        }
        [Authorize]
        public async Task<ViewResult> Jobs()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var model = new JobsViewModel
            {
                Jobs = await _context.Jobs.ToListAsync(),
                JobApplications = await _context.JobApplications
                                .Where(a => a.ApplicantId == userId).ToListAsync()
            };
            return View(model);
        }
        public ViewResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        




    }
}
