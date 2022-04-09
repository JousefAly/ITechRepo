using ITech.Data;
using ITech.Data.Repositories;
using ITech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ShoppingCart _shoppingCart;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderController(UserManager<AppUser> userManager,
                               ShoppingCart shoppingCart,
                               ICustomerRepository customerRepository,
                               IOrderRepository orderRepository)
        {
            _userManager = userManager;
            _shoppingCart = shoppingCart;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public ViewResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {

                order.User = await _userManager.GetUserAsync(HttpContext.User);

                if (_orderRepository.CreateOrder(order) == 0)
                {
                    TempData["StatusMessage"] = "Error: sorry we could not create your order";
                    return RedirectToAction(nameof(Checkout));
                }
                _shoppingCart.ClearCart();
                return RedirectToAction(nameof(CheckoutComplete));
            }
            return RedirectToAction(nameof(Checkout));
        }
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. Your will recieve it soon!";
            return View();
        }
        public ViewResult Orders(bool includeDetails = false)
        {

            return includeDetails ? View(_orderRepository.GetAllOrders(true)) : View(_orderRepository.GetAllOrders());
        }
        public RedirectToActionResult Accept(int id)
        {
            if( _orderRepository.GetById(id).Accepted || _orderRepository.Accept(id, _userManager.GetUserId(HttpContext.User)))
            {
                TempData["StatusMessage"] = "Order: " + id + ". Accepetd!";
                return RedirectToAction(nameof(Orders), new { includeDetails = true });
            }
            TempData["StatusMessage"] = "Error: Order: " + id + ". was not accepted!";
            return RedirectToAction(nameof(Orders), new { includeDetails = true });
        }
        public RedirectToActionResult Refuse(int id)
        {
            if (!_orderRepository.GetById(id).Accepted || _orderRepository.Refuse(id, _userManager.GetUserId(HttpContext.User)))
            {
                TempData["StatusMessage"] = "Order: " + id + ". Refused!";
                return RedirectToAction(nameof(Orders), new { includeDetails = true });
            }
            TempData["StatusMessage"] = "Error: Order: " + id + ". was not refused !";
            return RedirectToAction(nameof(Orders), new { includeDetails = true });
        }
    }
}
