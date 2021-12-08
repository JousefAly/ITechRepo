using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{

    public class Product
    {
        public Product()
        {
            DiscountPercentage = 0m;
            PriceAfterDiscount = Price - (Price * (DiscountPercentage / 100));
            LaunchTime = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime LaunchTime { get; set; }
        public string Image1Name { get; set; }
        public string Image2Name { get; set; }
        public string Image3Name { get; set; }
        public string Image4Name { get; set; }
        public string Image5Name { get; set; }
        public string Image6Name { get; set; }
        public string Image7Name { get; set; }
        public string Image8Name { get; set; }
        public bool InStock { get; set; }
        public int SoldCount { get; set; }

        public Category Category { get; set; }
        public Seller Seller { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }


    }

}
