using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string ITSIN { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
