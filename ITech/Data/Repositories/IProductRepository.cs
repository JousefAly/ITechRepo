using ITech.Data.Entites;
using System.Collections.Generic;

namespace ITech.Data.Repositories
{
    public interface IProductRepository
    {
        void Add(Product prodcut);
        void Add(IEnumerable<Product> products);
        List<Product> GetAllProducts();
        int SaveChanges();
        void AddProductDetail(Product prodcut, ProductDetail detail);
        Product GetProductByITSIN(string iTSIN);
        Product GetById(int id);
    }
}