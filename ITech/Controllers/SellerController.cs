using ITech.Data;
using ITech.Data.Entites;
using ITech.Data.Repositories;
using ITech.ViewModels;
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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CompleteRegister()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var seller = _sellerRepository.GetUserSeller(user);
            return View(seller);
        }
        [HttpPost]
        public IActionResult CompleteRegister(Seller seller)
        {

            if (string.IsNullOrEmpty(seller.Address))
            {
                ModelState.AddModelError("", "Enter your address");
            }
            if (ModelState.IsValid)
            {
                if (_sellerRepository.Update(seller))
                {
                    return RedirectToAction("Index");
                }
                TempData["errorMessage"] = "Could not update ";
                return RedirectToAction("CompleteRegister");
            }
            TempData["errorMessage"] = "Invalid Data";
            return RedirectToAction("CompleteRegister");
        }
        public async Task<IActionResult> ManageProducts()
        {
            var seller = _sellerRepository.GetUserSeller(await _userManager.GetUserAsync(HttpContext.User));
            var model = new ManageSellerProductsViewModel 
            {
                SellerId = seller.Id,
                Products = _sellerRepository.GetSellerProducts(seller)
            };
            return View(model);
        }
        
        
    }
}
