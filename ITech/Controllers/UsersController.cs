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
    }
}
