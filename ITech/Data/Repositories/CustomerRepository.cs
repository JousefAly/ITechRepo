using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Customer Create(AppUser user)
        {
            var customer = new Customer
            {
                User = user
            };
            _
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
