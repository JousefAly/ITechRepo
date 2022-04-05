using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Repositories
{
    public interface ISellerRepository
    {
        Seller Create(AppUser user);
        int SaveChanges();
        //return seller oject for a given user or null if not found
        Seller GetUserSeller(AppUser user);
        Seller GetBySellerId(string sellerId);
        // return true if updated
        bool Update(Seller seller);
        List<Product> GetSellerProducts(Seller seller);
        bool Activate(string id);
        bool DesActivate(string id);


    }
}
