using ITech.Data;
using ITech.Data.Entites;
using System.Collections.Generic;

namespace ITech.Models
{
    public class HomeIndexViewModel
    {
        public List<Category> AllCategories { get; set; }
        public ProductSoldAmount[] TopSellingProducts { get; set; }
    }
}
