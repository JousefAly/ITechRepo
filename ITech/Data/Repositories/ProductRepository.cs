using ITech.Data.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);

        }
        public void Add(IEnumerable<Product> products)
        {
            _context.Products.AddRange(products);

        }


        public void AddProductDetail(Product product, ProductDetail detail)
        {
            detail.ITSIN = product.ITSIN;
            detail.Product = product;
            
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductImages)
                .ToList();           
        }
        //return product with its details
        public Product GetById(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductByITSIN(string iTSIN)
        {
            return _context.Products.FirstOrDefault(p => p.ITSIN == iTSIN);
        }

        public List<Product> GetTopSellingProducts(int numberOfTProducts)
        {
            var products = _context.Products.OrderBy(p => p.Title).Take(numberOfTProducts).ToList();
            return products;

        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
