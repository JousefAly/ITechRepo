using ITech.Data.Entites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductRepository(ApplicationDbContext context,
                                 IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
            //first delete all product images from server then delete product which will delete all it's dependent
            //relations by cascading delete
            var product = GetById(productId);
            if (product.ProductImages != null && product.ProductImages.Any())
            {
                foreach (var p in product.ProductImages.ToList())
                {

                    DeleteProductImage(p.Id);

                }
            }

            //now delete product
            _context.Products.Remove(product);
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
        //upload image to server files then connect it with product in db
        //upload image to wwwroot/img/products then connect image with its product
        //change the image name to unique name with productId attached to it
        //return created image
        public async Task<ProductImage> AddProductImage(IFormFile imageFile, int imageNumber, int productId)
        {

            string wwwrootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string extension = Path.GetExtension(imageFile.FileName);
            string imageUniqueName = fileName + DateTime.Now.ToString("yymmssfff") + "-"
                                     + productId.ToString() + extension;
            var path = Path.Combine(wwwrootPath + "/img/products/", imageUniqueName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            //now image uploaded connect it with product in db

            var productImage = new ProductImage
            {
                ImageNumber = imageNumber,
                ImageUrl = "img/products/" + imageUniqueName,
                ProductId = productId
            };


            _context.Add(productImage);
            return _context.SaveChanges() > 0 ? productImage : null;
        }
        public ProductImage GetProductImage(int imageId)
        {

            return _context.ProductImages.Find(imageId);
        }
        public bool DeleteProductImage(int imageId)
        {
            //delete from files
            var image = GetProductImage(imageId);
            if (image == null)
            {
                return false;
            }
            string wwwrootPath = _hostEnvironment.WebRootPath;
            string imageName = image.ImageUrl.Substring(13);
            string path = Path.Combine(wwwrootPath + "/img/products/", imageName);
            File.Delete(path);
            if (File.Exists(path))
            {
                return false;
            }
            _context.ProductImages.Remove(image);
            return _context.SaveChanges() > 0;
        }

        public bool Activate(int id)
        {
            var product = _context.Products.Find(id);
            if (product.Activated)
                return true;
            product.Activated = true;
            return _context.SaveChanges() > 0;
        }

        public bool DesActivate(int id)
        {
            var product = _context.Products.Find(id);
            if (!product.Activated)
                return true;
            product.Activated = false;
            return _context.SaveChanges() > 0;
        }

        public async Task<int> DesActivateSellerPrdoucts(string sellerId)
        {
            var products = _context.Products.Where(p => p.SellerId == sellerId).ToList();
            if (products == null)
                return 0;
            var deactivatedProducts = products.Count(p => !p.Activated);
            foreach (var product in products)
            {
                product.Activated = false;
            }

            deactivatedProducts += await _context.SaveChangesAsync();
            return deactivatedProducts;
        }
        public async Task<int> ActivateSellerPrdoucts(string sellerId)
        {

            var products = _context.Products.Where(p => p.SellerId == sellerId).ToList();
            var activatedProducts = products.Count(p => p.Activated);
            if (products == null)
                return 0;
            foreach (var product in products)
            {
                product.Activated = true;
            }
            activatedProducts += await _context.SaveChangesAsync();
            return activatedProducts;
        }

        public ProductStats GetProductStats(int productId)
        {
            var product = _context.Products
                            .Include(p => p.ProductImages)
                            .Include(p => p.Seller)
                            .ThenInclude(s => s.User)
                            .FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return null;
            var soldCount = _context.OrderDetails
                                    .Where(od => od.ProductId == productId)
                                    .Sum(od => od.Amount);
            return new ProductStats
            {
                product = product,
                SoldCount = soldCount,
                TotalSoldAmount = soldCount * product.PriceAfterDiscount,
                CustomersUsernames = GetProductDistinctCustomers(productId)
            };

        }

        public string[] GetProductDistinctCustomers(int productId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.OrderDetails.Any(od => od.ProductId == productId))
                .Select(o => o.User.UserName)
                .Distinct()
                .ToArray();
        }

        public ProductStats[] GetSellerProductsStats(string sellerId)        
        {
            var products = _context.Products
                            .Include(p => p.ProductImages)
                            .Include(p => p.Seller)
                            .ThenInclude(s => s.User)
                            .Where(p => p.SellerId == sellerId)
                            .ToArray();
            if (products == null)
                return Array.Empty<ProductStats>();
            var productsStats = new ProductStats[products.Length];
            for(int i = 0; i < productsStats.Length; i++)
            {
                productsStats[i] = GetProductStats(products[i].Id);
                
            }
            return productsStats;
        }
    }
}
