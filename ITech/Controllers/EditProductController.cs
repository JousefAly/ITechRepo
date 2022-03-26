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
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View();
        }
        [HttpPost]
        public IActionResult EditMainInformation(Product product, int categoryId)
        {

            return View();
        }
        public IActionResult EditDetails(int productId)
        {
            return View();
        }
        public IActionResult EditImages(int productId)
        {
            return View();
        }
    }
}
