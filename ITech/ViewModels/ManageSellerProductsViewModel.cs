using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class ManageSellerProductsViewModel
    {
        public List<Product> Products { get; set; }
        public string SellerId { get; set; }
    }
}
