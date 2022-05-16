using ITech.Data;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SeederController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly ISellerRepository _sellerRepository;
        private readonly UserManager<AppUser> _userManager;

        public SeederController(ICategoryRepository categoryRepository,
                                IProductRepository productRepository,
                                ApplicationDbContext context,
                                ISellerRepository sellerRepository,
                                UserManager<AppUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _context = context;
            _sellerRepository = sellerRepository;
            _userManager = userManager;
        }
        public IActionResult SeedCategories(int desiredSeed)
        {
            Seeder seeder = new Seeder(_categoryRepository, _productRepository, _context);
            int rowsAffected = seeder.SeedCategories(desiredSeed);
            ViewBag.RowsAffected = rowsAffected.ToString();
            return View();
        }
        public IActionResult SeedProducts(int desiredSeed)
        {
            Seeder seeder = new Seeder(_categoryRepository, _productRepository, _context);
            int rowsAffected = seeder.SeedProducts(desiredSeed);
            ViewBag.RowsAffected = rowsAffected.ToString();
            return View();
        }
        public IActionResult AddStockToNonStockProducts(int amount)
        {
            var products = _context.Products.Where(p => p.Stock <= 0).ToArray();
            foreach (var p in products)
            {
                p.Stock += amount;
            }
            ViewBag.RowsAffected = _context.SaveChanges().ToString();
            return View();
        }
        public async Task<IActionResult> AddDefaultSeller()
        {
            var defaultSeller = new AppUser
            {
                UserName = "DefaultSeller",

                Email = "DefaultSeller@Default.com",
                PhoneNumber = "0111111111",
                FirstName = "DefaultSellerFirstName",
                LastName = "DefaultSellerLastName"
            };

            var result = await _userManager.CreateAsync(defaultSeller, "123456");
            if (!result.Succeeded)
            {
                ViewBag.Message = "Didn't Create default user";
                return View(defaultSeller);
            }


            var seller =  _sellerRepository.Create(defaultSeller);
            ViewBag.Message = "Created Default Seller successfully";
            if (seller == null)
            {
                ViewBag.Message = "Error: Created Default User but did not create seller";
                ViewBag.SellerId = "";
                return View(defaultSeller);
            }
            seller.Activated = true;
            seller.Address = "Default Address";
            _context.SaveChanges();
            ViewBag.SellerId = seller.Id;
            return View(defaultSeller);

        }
        public async Task<IActionResult> AddDefaultSellerToProductsWithoutSeller()
        {
            var user = await _userManager.FindByNameAsync("DefaultSeller");
            if (user == null)
            {
                ViewBag.Message = "DefaultUser not found";
                return View();
            }
            var seller = _sellerRepository.GetUserSeller(user);
            if (seller == null)
            {
                ViewBag.Message = "DefaultUser Found but Seller Not Found";
                return View();
            }
            var products = _context.Products.Where(p => p.Seller == null).ToArray();
            foreach (var product in products)
            {
                product.Seller = seller;
            }
            ViewBag.Message = _context.SaveChanges() + " Rows Affected.";

            return View();
        }
    }
}
