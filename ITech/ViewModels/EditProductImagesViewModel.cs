using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class EditProductImagesViewModel
    {
        public int ProductId { get; set; }
        public string StatusMessage { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
