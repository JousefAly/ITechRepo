using ITech.Data.Entites;
using System.Collections.Generic;

namespace ITech.Data.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product prodcut);
        List<Product> GetAllProducts();
        int SaveChanges();
    }
}