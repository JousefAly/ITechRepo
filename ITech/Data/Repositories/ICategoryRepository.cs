using ITech.Data.Entites;

namespace ITech.Data.Repositories
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        int SaveChanges();
    }
}