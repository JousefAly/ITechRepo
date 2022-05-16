using ITech.Data;
using ITech.Data.Entites;
using ITech.Data.Repositories;
using ITech.Models;
using ITech.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISellerRepository _sellerRepository;
        private readonly IProductRepository _productRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
                                ISellerRepository sellerRepository,
                                IProductRepository productRepository,
                                INotificationRepository notificationRepository,
                                IOrderRepository orderRepository,
                                ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _sellerRepository = sellerRepository;
            _productRepository = productRepository;
            _notificationRepository = notificationRepository;
            _orderRepository = orderRepository;
            _context = context;
        }

        public async Task<IActionResult> Index(string idFilter = "")
        {
            if (!string.IsNullOrEmpty(idFilter))
            {
                var user = await _userManager.FindByIdAsync(idFilter);
                var userModel = new List<UserViewModel>
                {
                    new UserViewModel
                    {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = _userManager.GetRolesAsync(user).Result
                    }

                };

                return View(userModel);
            }
            var users = await _userManager.Users.Select(usr => new UserViewModel
            {
                Id = usr.Id,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                UserName = usr.UserName,
                Email = usr.Email,
                Roles = _userManager.GetRolesAsync(usr).Result
            }).ToListAsync();

            return View(users);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            string userEmail = user.Email;
            //on Delete User set it's assocciated FK to null in fluent API
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["StatusMessage"] = "User : " + userEmail + ". Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["StatusMessage"] = "Error: User : " + userEmail + ". was not deleted!";
                return RedirectToAction(nameof(Index));
            }

        }
        public async Task<ViewResult> ManageRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var model = new ManageUserRolesViewModel
            {
                UserId = id,
                Username = user.UserName,
                RoleNames = _roleManager.Roles.Select(r => r.Name).ToArray(),
                UserRoleNames = await _userManager.GetRolesAsync(user)
            };
            return View(model);
        }



        public async Task<RedirectToActionResult> AddToRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                TempData["StatusMessage"] = "Error: Could not Assign (" + roleName + ") to userId: " + userId;
                return RedirectToAction(nameof(ManageRoles), new { id = userId });
            }
            TempData["StatusMessage"] = "Successfully Assigned (" + roleName + ") to userId: " + userId;
            var message = "You are Assigned as " + roleName + ".";
            await _notificationRepository.NotifyAsync(_userManager.GetUserId(HttpContext.User), user.Id, message);


            return RedirectToAction(nameof(ManageRoles), new { id = userId });
        }

        public async Task<RedirectToActionResult> RemoveFromRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                TempData["StatusMessage"] = "Error: Could not Remove (" + roleName + ") from userId: " + userId;
                return RedirectToAction(nameof(ManageRoles), new { id = userId });
            }
            TempData["StatusMessage"] = "Successfully Removed (" + roleName + ") from userId: " + userId;
            return RedirectToAction(nameof(ManageRoles), new { id = userId });
        }
        //Bad Code Divide action into sub actions
        public async Task<ViewResult> Manage(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (await _userManager.IsInRoleAsync(user, "Seller"))
            {
                var seller = _sellerRepository.GetUserSeller(user);

                var model = new ManageUserViewModel
                {
                    User = user,
                    Seller = _sellerRepository.GetUserSeller(user, includeProducts: true),
                    SellerProductStats = _productRepository.GetSellerProductsStats(seller.Id),
                    Orders = _orderRepository.GetUserOrders(id, includeDetails: true)
                };
                return View("ManageSeller", model);
            }
            else if (await _userManager.IsInRoleAsync(user, "Clerk"))
            {
                var model = new ManageUserViewModel
                {
                    User = user,
                    ClerkManagedOrders = _orderRepository.GetHandlerOrders(id, includeDetails: true),
                    Orders = _orderRepository.GetUserOrders(id, includeDetails: true)
                };
                return View("ManageClerk", model);
            }
            else
            {
                var model = new ManageUserViewModel
                {
                    User = user,                    
                    Orders = _orderRepository.GetUserOrders(id, includeDetails: true)
                };
                return View("ManageDefaultUser", model);
            }
        }
        public RedirectToActionResult ActivateSeller(string id)
        {
            var seller = _context.Sellers.FirstOrDefault(s => s.UserId == id) ?? new Seller { UserId = id };
            if (seller.Activated)
            {
                TempData["StatusMessage"] = "Seller Already Activated  seller id: " + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }
            seller.Activated = true;
            if (_context.SaveChanges() > 0)
            {
                TempData["StatusMessage"] = "Seller Activated Successfully seller id: " + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }

            TempData["StatusMessage"] = "Error: Seller was not Activated  user id: " + id;
            return RedirectToAction(nameof(Manage), new { id });

        }
        public async Task<RedirectToActionResult> DesActivateSeller(string id)
        {
            var seller = _context.Sellers.FirstOrDefault(s => s.UserId == id);
            if (seller == null)
            {
                TempData["StatusMessage"] = "Error: Seller is not in the system" + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }
            if (!seller.Activated)
            {
                TempData["StatusMessage"] = "Seller is not Activated already seller Id: " + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }
            var desActivatedProductsNum = await _productRepository.DesActivateSellerPrdoucts(seller.Id);
            seller.Activated = false;
            if (_context.SaveChanges() > 0)
            {
                TempData["StatusMessage"] = "Seller Disactivated Successfully seller id: " + seller.Id + ". " +
                    "(" + desActivatedProductsNum + ") products DesActivated.";
                return RedirectToAction(nameof(Manage), new { id });
            }

            TempData["StatusMessage"] = "Error: Could not disactivate seller id: " + seller.Id;
            return RedirectToAction(nameof(Manage), new { id });

        }
        public async Task<RedirectToActionResult> DesActivateSellerProducts(string id)
        {
            var seller = _context.Sellers.FirstOrDefault(s => s.UserId == id);
            if (seller == null)
            {
                TempData["StatusMessage"] = "Error: Seller is not in the system" + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }

            if (await _productRepository.DesActivateSellerPrdoucts(seller.Id) > 0)
            {
                TempData["StatusMessage"] = "Products Disactivated Successfully seller id: " + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }

            TempData["StatusMessage"] = "Error: Could not disactivate  Products. seller id: " + seller.Id;
            return RedirectToAction(nameof(Manage), new { id });

        }
        public async Task<RedirectToActionResult> ActivateSellerProducts(string id)
        {
            var seller = _context.Sellers.FirstOrDefault(s => s.UserId == id);
            if (seller == null)
            {
                TempData["StatusMessage"] = "Error: Seller is not in the system" + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }
            if (!seller.Activated)
            {
                TempData["StatusMessage"] = "Error: Can't Activate Products because Seller is deactivated. seller Id: " + seller.Id;
                return RedirectToAction(nameof(Manage), new { id });
            }
            var activatedProductsNum = await _productRepository.ActivateSellerPrdoucts(seller.Id);

            if (activatedProductsNum > 0)
            {
                TempData["StatusMessage"] = "Products Activated Successfully seller id: " + seller.Id +
                    ". (" + activatedProductsNum + ") product activated.";
                return RedirectToAction(nameof(Manage), new { id });
            }

            TempData["StatusMessage"] = "Error: Could not activate  Products. seller id: " + seller.Id;
            return RedirectToAction(nameof(Manage), new { id });

        }
        public async Task<IActionResult> Notify(string id)
        {
            var notification = new Notification
            {
                ReceiverId = id,
                Receiver = await _userManager.FindByIdAsync(id),
                SenderId = _userManager.GetUserId(HttpContext.User)
            };
            return View(notification);
        }
        [HttpPost]
        public async Task<IActionResult> Notify(Notification model)
        {
            if (string.IsNullOrEmpty(model.Message))
            {
                TempData["StatusMessage"] = "Error: Invalid Message.";
                return RedirectToAction(nameof(Notify), new { id = model.ReceiverId });
            }
            var notificationId = await _notificationRepository.NotifyAsync(model.SenderId, model.ReceiverId, model.Message);
            if (string.IsNullOrEmpty(notificationId))
            {
                TempData["StatusMessage"] = "Error: Could not notify user";
                return RedirectToAction(nameof(Index));
            }
            TempData["StatusMessage"] = "Notified User: " + model.ReceiverId + ". Notification Id: " + notificationId;
            return RedirectToAction(nameof(Index));
        }

    }
}
