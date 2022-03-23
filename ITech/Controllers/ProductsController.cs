using ITech.Data;
using ITech.Data.Entites;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly UserManager<AppUser> _userManager;

        public ProductsController(IProductRepository productRepository,
                                  ISellerRepository sellerRepository,
                                  UserManager<AppUser> userManager)
        {
            _productRepository = productRepository;
            _sellerRepository = sellerRepository;
            _userManager = userManager;
        }

        //return all products   
        public IActionResult Index()
        {

            return View(_productRepository.GetAllProducts());
        }
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);

            return View(product);
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var appUser = await _userManager.GetUserAsync(HttpContext.User);
            var seller = _sellerRepository.GetUserSeller(appUser);
            var createdProduct = _productRepository.AddSellerProduct(seller, product);
            if (createdProduct == null)
                return BadRequest("Product was not created.");
            
            return RedirectToAction(nameof(CreateProductImages), new { productId =createdProduct.Id });
        }
        public IActionResult CreateProductImages(int productId)
        {
            var product = _productRepository.GetById(productId);
            return View(product);
        }
    }
}
