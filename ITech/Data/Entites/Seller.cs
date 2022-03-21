using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ITech.Data.Entites
{
    public class Seller 
    {
        public Seller()
        {
            ProductsCount = 0;
        }
        public string Id { get; set; }
        public AppUser User { get; set; }
        public int ProductsCount { get; set; } 
        public List<Product> Products { get; set; }
    }
}