using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class EditProductDetailsViewModel
    {
        public int ProductId { get; set; }
        public ProductDetail DetailToEdit { get; set; }
        public string StatusMessage { get; set; }
        public List<ProductDetail>  ProductDetails { get; set; }
    }
}
