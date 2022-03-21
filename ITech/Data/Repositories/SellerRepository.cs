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
    }
}
