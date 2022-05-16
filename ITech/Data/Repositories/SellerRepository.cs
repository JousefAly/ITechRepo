using ITech.Data.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApplicationDbContext _context;

        public SellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Seller Create(AppUser user)
        {
            var seller = new Seller
            {
                Id = Guid.NewGuid().ToString(),
                User = user
            };
            _context.Sellers.Add(seller);
            return _context.SaveChanges() > 0 ? seller : null;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        //return the seller object for a given appuser or null if not seller
        public Seller GetUserSeller(AppUser user, bool includeProducts = false)
        {
            var seller = includeProducts ? _context.Sellers.Include(s => s.Products).FirstOrDefault(s => s.User == user) :
                _context.Sellers.FirstOrDefault(s => s.User == user);
            return seller;
        }
        public bool Update(Seller seller)
        {
            _context.Update(seller);

            return _context.SaveChanges() > 0;
        }
        public List<Product> GetSellerProducts(Seller seller)
        {
            return _context.Products
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductImages)
                .Where(p => p.Seller == seller).ToList();
        }

        public Seller GetBySellerId(string sellerId)
        {
            return _context.Sellers.Find(sellerId);
        }

        public bool Activate(string id)
        {
            var seller = _context.Sellers.Find(id);
            if (seller.Activated)
                return true;
            seller.Activated = true;
            return _context.SaveChanges() > 0;
        }

        public bool DesActivate(string id)
        {
            var seller = _context.Sellers.Find(id);
            if (!seller.Activated)
                return true;
            seller.Activated = false;
            return _context.SaveChanges() > 0;
        }
    }
}
