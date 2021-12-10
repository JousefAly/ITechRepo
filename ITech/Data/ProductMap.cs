using CsvHelper.Configuration;
using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data
{
    public class ProductMap: ClassMap<Product>
    {
        public ProductMap()
        {
            Map(p => p.Title).Name("Title");
            Map(p => p.Image1Name).Name("Image1Name");
            Map(p => p.Image2Name).Name("Image2Name");
            Map(p => p.Image3Name).Name("Image3Name");
            Map(p => p.Image4Name).Name("Image4Name");
            Map(p => p.Image5Name).Name("Image5Name");
            Map(p => p.Image6Name).Name("Image6Name");
            Map(p => p.Image7Name).Name("Image7Name");
            Map(p => p.Image8Name).Name("Image8Name");
            Map(p => p.Brand).Name("Brand");
            Map(p => p.Price).Name("Price");
            Map(p => p.ShortDescription).Name("ShortDescription");
            Map(p => p.ITSIN).Name("ITSIN");
            Map(p => p.Category.Id).Name("CategoryId");
        }
    }
}
