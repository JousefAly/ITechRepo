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
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);

        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Category).ToList();           
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
