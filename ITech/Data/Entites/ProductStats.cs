using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{
    public class ProductStats
    {
        public Product Product { get; set; }
        public int AcceptedOrders { get; set; }
        public int SoldCount { get; set; }
        public decimal TotalSoldAmount { get; set; }
        public string[] CustomersUsernames { get; set; }
    }
}
