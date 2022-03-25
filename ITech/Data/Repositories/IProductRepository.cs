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
        ProductDetail AddProductDetail(Product prodcut, ProductDetail detail);
        Product GetProductByITSIN(string iTSIN);
        Product GetById(int id);
        //return top products
        //numberOfProducts is how many top products we want to return
        List<Product> GetTopSellingProducts(int numberOfTProducts);
        //return Added product
        Product AddSellerProduct(Seller seller, Product product);
        ProductImage AddProductImage(Product product, ProductImage image);
        bool HasMainImage(int productId);
        bool Delete(int productId);
        
    }
}