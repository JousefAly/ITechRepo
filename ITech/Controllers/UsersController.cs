using ITech.Data;
using ITech.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
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
                return RedirectToAction(nameof(ManageRoles), new { id = userId});
            }
            TempData["StatusMessage"] = "Successfully Assigned (" + roleName + ") to userId: " + userId;
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

    }
}
