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
                Id = Guid.NewGuid().ToString(),
                User = user
            };
            _context.Customers.Add(customer);
            return customer;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
