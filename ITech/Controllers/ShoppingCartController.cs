using ITech.Data.Repositories;
using ITech.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(ShoppingCart shoppingCart, IProductRepository productRepository)
        {
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            ViewBag.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            return View();
        }
        public ActionResult AddToCart(int productId, int amount = 1)
        {
            _shoppingCart.AddToCart(_productRepository.GetById(productId), amount);
            return RedirectToAction("Index","Home");
        }
    }
}
