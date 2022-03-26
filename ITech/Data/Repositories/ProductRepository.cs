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


        public ProductDetail AddProductDetail(Product product, ProductDetail detail)
        {

            product.ProductDetails.Add(detail);

            return _context.SaveChanges() > 0 ? detail : null;

        }

        public ProductImage AddProductImage(Product product, ProductImage image)
        {
            product.ProductImages.Add(image);
            if (_context.SaveChanges() == 0)
                return null;
            return image;
        }

        public Product AddSellerProduct(Seller seller, Product product)
        {
            product.Seller = seller;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool Delete(int productId)
        {
            _context.Products.Remove(_context.Products.Find(productId));

            return _context.SaveChanges() > 0;                
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
            var products = _context.Products.Include(p => p.ProductImages)
                                            .OrderBy(p => p.Title)
                                            .Take(numberOfTProducts).ToList();
            return products;

        }

        public bool HasMainImage(int productId)
        {
           
            return _context.Products.Include(p => p.ProductImages)
                                    .FirstOrDefault(p => p.Id == productId).ProductImages
                                    .FirstOrDefault(pi => pi.ImageNumber == 1) != null;
        }

       
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public List<Product> GetSellerProducts(Seller seller)
        {
            return _context.Products
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductImages)
                .Where(p => p.Seller == seller).ToList();
        }

        public Product Update(Product product)
        {            
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }
        public ProductDetail UpdateProductDetail(ProductDetail productDetail)
        {
            _context.Update(productDetail);
            return _context.SaveChanges() > 0 ? productDetail : null;
        }

        public bool DeleteProductDetail(int detailId)
        {
            
            _context.Remove(_context.ProductDetails.Find(detailId));
            return _context.SaveChanges() > 0;
        }
    }
}
