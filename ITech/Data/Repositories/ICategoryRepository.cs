using ITech.Data.Entites;
using System.Collections.Generic;

namespace ITech.Data.Repositories
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        Category GetCategoryByName(string name);
        int SaveChanges();
        Category GetCategoryById(int id);
        List<Category> GetAllCategories();
    }
}