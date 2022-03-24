using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{
    public class ProductImage
    {
        //ex images 1,2,3 of product 5
        public int Id { get; set; }
        public int ImageNumber { get; set; }
        public string ImageUrl { get; set; }
        public Product Product { get; set; }
    }
}
