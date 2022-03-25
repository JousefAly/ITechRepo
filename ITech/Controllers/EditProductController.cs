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

        public EditProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index(int productId)
        {
            ViewData["productId"] = productId;
            return View(_productRepository.GetById(productId));
        }
        public IActionResult EditMainInformation(int productId)
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
