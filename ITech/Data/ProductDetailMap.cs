using CsvHelper.Configuration;
using ITech.Data.Entites;

namespace ITech.Data
{
    public class ProductDetailMap : ClassMap<ProductDetail>
    {
        public ProductDetailMap()
        {
            Map(d => d.ITSIN).Name("ITSIN");
            Map(d => d.Title).Name("Title");
            Map(d => d.Content).Name("Content");
        }
    }
}