using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ITech.Data.Entites
{
    public class Seller 
    {        
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool Activated { get; set; }
        public string Address { get; set; }
        public AppUser User { get; set; }                
        public List<Product> Products { get; set; }
    }
}