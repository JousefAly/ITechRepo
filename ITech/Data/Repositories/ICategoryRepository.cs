using ITech.Data.Entites;

namespace ITech.Data.Repositories
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        Category GetCategoryByName(string name);
        int SaveChanges();
    }
}