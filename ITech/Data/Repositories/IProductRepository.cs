using ITech.Data.Entites;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public interface IProductRepository
    {
        void Add(Product prodcut);
        void Add(IEnumerable<Product> products);
        //return Added product
        Product AddSellerProduct(Seller seller, Product product);
        List<Product> GetAllProducts();
        int SaveChanges();
        ProductDetail AddProductDetail(Product prodcut, ProductDetail detail);
        Product GetProductByITSIN(string iTSIN);
        Product GetById(int id);

        Product[] GetProductsByCategory(string categoryName, bool includeDetails = false);
        List<Product> GetSellerProducts(Seller seller);
        ProductImage AddProductImage(Product product, ProductImage image);
        bool HasMainImage(int productId);
        bool Delete(int productId);
        Product Update(Product product);
        ProductDetail UpdateProductDetail(ProductDetail productDetail);
        bool DeleteProductDetail(int detailId);
        ProductImage GetProductImage(int imageId);
        //upload image to server files then connect it with product in db
        //return created image
        public Task<ProductImage> AddProductImage(IFormFile imageFile, int imageNumber, int productId);

        //Delete from files and delete record from database
        //return true if deleted from both database and files
        public bool DeleteProductImage(int imageId);
        bool Activate(int id);
        bool DesActivate(int id);
        //return number of desactivated products
        Task<int> DesActivateSellerPrdoucts(string sellerId);
        //return number of desactivated products
        Task<int> ActivateSellerPrdoucts(string sellerId);
        ProductStats GetProductStats(int productId);
        ProductStats[] GetSellerProductsStats(string sellerId);
        string[] GetProductDistinctCustomers(int productId);
        //return current Stock
        int AddToStock(int productId, int amount);
        //return current stock
        int RemoveFromStock(int productId, int amount);
        Product[] Search(string searchString);
        // Get the most recent order products
        Product[] TrendyProducts(int numberOfProducts);
        ProductSoldAmount[] GetTopSellingProducts(int numberOfProducts, bool includeImagesAndDetails = false);

        // save product if not saved and return whether product is saved
        bool SaveProduct(string userId, int productId);
        Product[] GetUserSavedProducts(string userId);
    }
}