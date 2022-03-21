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
    public class SellerController : Controller
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly UserManager<AppUser> _userManager;

        public SellerController(ISellerRepository sellerRepository,
                                UserManager<AppUser> userManager)

        {
            _sellerRepository = sellerRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> CompleteRegister()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var seller = _sellerRepository.GetUserSeller(user);
            return View(seller);
        }
    }
}
