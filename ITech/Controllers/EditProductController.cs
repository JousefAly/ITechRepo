using ITech.Data.Entites;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
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

        public EditProductController(IProductRepository productRepository,
                                     ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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
            product.LaunchTime = _productRepository.GetById(product.Id).LaunchTime;
            product.Category = _categoryRepository.GetCategoryById(categoryId);
            _productRepository.Update(product);
            return View();
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
