using ITech.Data.Entites;

namespace ITech.Data.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product prodcut);
        int SaveChanges();
    }
}