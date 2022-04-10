using ITech.Data;
using ITech.Data.Entites;
using ITech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class ManageUserViewModel
    {
        public AppUser User { get; set; }
        public Seller Seller { get; set; }
        public Order[] SellerOrders { get; set; }
        public Order[] ClerkManagedOrders { get; set; }
        public Order[] Orders { get; set; }
    }
}
