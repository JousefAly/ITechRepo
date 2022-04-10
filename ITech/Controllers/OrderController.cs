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
        private readonly INotificationRepository _notificationRepository;

        public OrderController(UserManager<AppUser> userManager,
                               ShoppingCart shoppingCart,
                               ICustomerRepository customerRepository,
                               IOrderRepository orderRepository,
                               INotificationRepository notificationRepository)
        {
            _userManager = userManager;
            _shoppingCart = shoppingCart;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _notificationRepository = notificationRepository;
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
                var orderId = _orderRepository.CreateOrder(order);

                if (orderId == 0)
                {
                    TempData["StatusMessage"] = "Error: sorry we could not create your order";
                    return RedirectToAction(nameof(Checkout));
                }
                var admin = await _userManager.FindByNameAsync("admin");
                var message = "You placed Your order Successfully. order Id = " + orderId
                        + ". Order Total: " + order.OrderTotal.ToString("c");
                await _notificationRepository.Notify(admin.Id, order.User.Id, message);
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
        [Authorize(Roles = "Admin,Clerk")]
        public RedirectToActionResult Accept(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order.Accepted || _orderRepository.Accept(id, _userManager.GetUserId(HttpContext.User)))
            {
                var message = "Your order : " + order.OrderId + ", With Total amount" + order.OrderTotal.ToString("c") +
                    ". Accepted Successfully. Order will arive soon!";
                _notificationRepository.Notify(_userManager.GetUserId(HttpContext.User), order.UserId, message);
                TempData["StatusMessage"] = "Order: " + id + ". Accepetd!";
                return RedirectToAction(nameof(Orders), new { includeDetails = true });
            }
            TempData["StatusMessage"] = "Error: Order: " + id + ". was not accepted!";
            return RedirectToAction(nameof(Orders), new { includeDetails = true });
        }
        [Authorize(Roles = "Admin,Clerk")]
        public RedirectToActionResult Refuse(int id)
        {
            var order = _orderRepository.GetById(id);
            if (!order.Accepted || _orderRepository.Refuse(id, _userManager.GetUserId(HttpContext.User)))
            {
                var message = "Your order : " + order.OrderId + ", With Total amount" + order.OrderTotal.ToString("c") +
                   " is refused unfortunaltely!";
                _notificationRepository.Notify(_userManager.GetUserId(HttpContext.User), order.UserId, message);
                TempData["StatusMessage"] = "Order: " + id + ". Refused!";
                return RedirectToAction(nameof(Orders), new { includeDetails = true });
            }
            TempData["StatusMessage"] = "Error: Order: " + id + ". was not refused !";
            return RedirectToAction(nameof(Orders), new { includeDetails = true });
        }
    }
}
