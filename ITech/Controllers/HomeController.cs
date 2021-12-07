using ITech.Data.Repositories;
using ITech.Models;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger,
                                IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {

            return View(_productRepository.GetAllProducts());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UpdateNames()
        {
            var products = _productRepository.GetAllProducts();
            int affectedRows = 0;
            for (int i = 1; i <= products.Count; i++)
            {
                if (i == 1 || i == 6)
                {


                    products[i - 1].Image1Url = "/img/mockImages/Mock1.1";
                    products[i - 1].Image2Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image3Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image4Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image5Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image6Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image7Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image8Url = "/img/mockImages/EmptyMock";
                }
                else if (i > 5)
                {
                    products[i - 1].Image1Url = "/img/mockImages/Mock" + (i - 5) + ".1";
                    products[i - 1].Image2Url = "/img/mockImages/Mock" + (i - 5) + ".2";
                    products[i - 1].Image3Url = "/img/mockImages/Mock" + (i - 5) + ".3";
                    products[i - 1].Image4Url = "/img/mockImages/Mock" + (i - 5) + ".4";
                    products[i - 1].Image5Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image6Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image7Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image8Url = "/img/mockImages/EmptyMock";
                }
                else
                {
                    products[i - 1].Image1Url = "/img/mockImages/Mock" + i + ".1";
                    products[i - 1].Image2Url = "/img/mockImages/Mock" + i + ".2";
                    products[i - 1].Image3Url = "/img/mockImages/Mock" + i + ".3";
                    products[i - 1].Image4Url = "/img/mockImages/Mock" + i + ".4";
                    products[i - 1].Image5Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image6Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image7Url = "/img/mockImages/EmptyMock";
                    products[i - 1].Image8Url = "/img/mockImages/EmptyMock";
                }

                _productRepository.SaveChanges();
                affectedRows += 1;
            }
            ViewBag.AffectedRows = affectedRows;
            return View();
        }
    }
}
