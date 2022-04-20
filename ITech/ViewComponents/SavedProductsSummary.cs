using ITech.Data;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITech.ViewComponents
{
    public class SavedProductsSummary : ViewComponent
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;

        public SavedProductsSummary(IProductRepository productRepository,
                                    UserManager<AppUser> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
             return View(_productRepository.GetUserSavedProducts(_userManager.GetUserId(HttpContext.User)).Length);
        }
    }
}
