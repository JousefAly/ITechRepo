using ITech.Data.Repositories;
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

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
    }
}
