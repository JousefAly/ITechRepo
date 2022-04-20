using ITech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            LaunchTime = DateTime.Now;
        }
        private decimal _Price;
        private decimal _PriceAfterDiscount;
        public int Id { get; set; }

        public string Title { get; set; }
        public bool Activated { get; set; }

        public string ITSIN { get; set; }
       

        public string Brand { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                _PriceAfterDiscount = _Price - (_Price * (DiscountPercentage / 100));
            }
        }
        public decimal PriceAfterDiscount { get { return _PriceAfterDiscount; } set { _PriceAfterDiscount = value; } }
        public decimal DiscountPercentage { get; set; }
        public DateTime LaunchTime { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string SellerId { get; set; }
        public Seller Seller { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<AppUser> SavingUsers { get; set; }


    }

}
