using ITech.Data.Repositories;
using ITech.Models;
using ITech.ViewModels;
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
            var model = new ShoppingCartViewModel
            {
                ShoppingCartItems = _shoppingCart.GetShoppingCartItems(),
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(model);
        }
        public RedirectToActionResult AddToCart(int productId, int amount = 1)
        {
            _shoppingCart.AddToCart(_productRepository.GetById(productId), amount);
            return RedirectToAction(nameof(Index));
        }
        public RedirectToActionResult RemoveFromCart(int productId, int amount = 1)
        {
            _shoppingCart.RemoveFromCart(_productRepository.GetById(productId), amount);
            return RedirectToAction(nameof(Index));
        }
        public RedirectToActionResult ResetCart(int productId, int amount = 1)
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index", "Home");
        }
    }
}
