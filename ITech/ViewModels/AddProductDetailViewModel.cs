using ITech.Data.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class AddProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
    }
}
