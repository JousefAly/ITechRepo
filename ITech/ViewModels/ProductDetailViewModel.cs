using ITech.Data.Entites;
using ITech.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public double TotalRating { get; set; } = 0.0;
        public int RatingCount { get; set; }
        public List<YoutubeVideo> YoutubeVideos { get; set; }
        public bool ProductRatedByUser { get; set; }
    }
}
