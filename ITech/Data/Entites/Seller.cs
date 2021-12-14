using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ITech.Data.Entites
{
    public class Seller :IdentityUser
    {
        //IdentityUser already has Id prop
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Product> Products { get; set; }
    }
}