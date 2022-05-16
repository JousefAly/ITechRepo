using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public interface ICustomerRepository
    {
        Customer Create(AppUser user);
        int SaveChanges();

    }
}
