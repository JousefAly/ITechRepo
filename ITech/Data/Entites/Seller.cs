using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ITech.Data.Entites
{
    public class Seller :AppUser
    {
       
        public List<Product> Products { get; set; }
    }
}