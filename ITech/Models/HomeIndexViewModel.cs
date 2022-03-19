using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Models
{
    public class HomeIndexViewModel
    {
        public List<Category> AllCategories { get; set; }
        public List<Product> TrendyProducts { get; set; }
    }
}
