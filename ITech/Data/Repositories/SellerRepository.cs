using ITech.Data.Entites;
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
            return seller;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        //return the seller object for a given appuser or null if not seller
        public Seller GetUserSeller(AppUser user)
        {
            var seller = _context.Sellers.FirstOrDefault(s => s.User == user);
            return seller;
        }
        public bool Update(Seller seller)
        {
            _context.Update(seller);

            return _context.SaveChanges() > 0 ;
        }
        
    }
}
