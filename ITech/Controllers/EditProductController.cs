using ITech.Data;
using ITech.Data.Entites;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class EditProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;

        public EditProductController(IProductRepository productRepository,
                                     ICategoryRepository categoryRepository,
                                     ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }
        public IActionResult Index(int productId)
        {
            ViewData["productId"] = productId;
            return View(_productRepository.GetById(productId));
        }
        public IActionResult EditMainInformation(int productId)
        {
            ViewData["productId"] = productId;
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(_productRepository.GetById(productId));
        }
        [HttpPost]
        public IActionResult EditMainInformation(Product product, int categoryId)
        {
            //make sure to not track the old product
            var oldProduct = _productRepository.GetById(product.Id);
            _context.Entry(oldProduct).State = EntityState.Detached;

            product.LaunchTime = oldProduct.LaunchTime;
            product.Category = _categoryRepository.GetCategoryById(categoryId);
            _productRepository.Update(product);
            return RedirectToAction(nameof(EditMainInformation), new { productId = product.Id});
        }
        public IActionResult EditDetails(int productId)
        {
            ViewData["productId"] = productId;
            return View();
        }
        public IActionResult EditImages(int productId)
        {
            ViewData["productId"] = productId;
            return View();
        }
    }
}
