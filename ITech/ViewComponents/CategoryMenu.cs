using ITech.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ITech.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {

            var categories = _categoryRepository.GetAllCategories().OrderBy(c => c.Name);
            return View(categories);
        }
    }
}
