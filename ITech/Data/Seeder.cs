using ITech.Data.Entites;
using ITech.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data
{
    public class Seeder
    {
        private readonly ICategoryRepository _categoryRepository;
        private static int NumOfCategorySeeds;
        private static int NumOfProductSeeds;
        public Seeder(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        //this method seed products 
        //desiredSeed is which seed you want
        //return affected rows in Database
        public int SeedProducts(int desiredSeed)
        {
            if (desiredSeed == 1)
            {

                if (NumOfProductSeeds == 0)
                {
                    NumOfProductSeeds += 1;

                    return SeedProductsInDbManually();
                }
            }
            return 0;
        }

        //this method seed Categories 
        //desiredSeed is which seed you want
        //return affected rows in Database
        public int SeedCategories(int desiredSeed)
        {
            if (desiredSeed == 1)
            {

                if (NumOfCategorySeeds == 0)
                {
                    NumOfCategorySeeds += 1;
                    return SeedCategoriesInDbManually();
                }
            }
            return 0;
        }
        //seed then return affected rows
        private int SeedCategoriesInDbManually()
        {
            var categories = new List<Category>()
            {
                new Category
                {
                  Name = "Phones",
                  Description = "This category includes all phones in website",
                },
                new Category
                {
                    Name = "Laptops",
                    Description = "This category includes all laptops in webiste"
                }
            };
            foreach (var category in categories)
            {
                _categoryRepository.AddCategory(category);
            }
            return _categoryRepository.SaveChanges();
        }
        //seed then return affected rows
        private int SeedProductsInDbManually()
        {
            var categories = new List<Product>()
            {
                new Product
                {
                  Name = "Nokia C1 - 5.45-inch 16GB/1GB Dual SIM 3G Mobile Phone - Charcoal",
                  ShortDescription = "Level up to the new Nokia C1. Enjoy your entertainment – anytime, anywhere – with the large screen and all-day battery life. Raise your selfie game with the front-facing flash and 5 MP camera. Do more with Android 9 Pie (Go edition) – watch your favorite videos, view your photos and even find your way home all while offline.",
                  Price = 1059m,
                  Image1Url = "~/img/mockImages/Mock1.1",
                  Image2Url = "~/img/mockImages/MockEmpty",
                  Image3Url = "~/img/mockImages/MockEmpty",
                  Category = _categoryRepository.GetCategoryByName("Phones"),
                  ProductDetails = new List<ProductDetail>()
                  {
                      new ProductDetail
                      {
                          Title = "Display Size",
                          Content = "5.45 inches, 76.7 cm2 (~72.7% screen-to-body ratio)"
                      },
                       new ProductDetail
                      {
                          Title = "Display Resolution",
                          Content = "480 x 960 pixels, 18:9 ratio (~197 ppi density)"
                      },
                        new ProductDetail
                      {
                          Title = "Display Type",
                          Content = "IPS LCD capacitive touchscreen, 16M colors"
                      }
                  },



                },
                new Product
                {
                  Name = "XIAOMI Redmi 9T - 6.53-inch 128GB/6GB Dual SIM Mobile Phone - Twilight Blue",
                  ShortDescription = "Brighten up your life with Redmi 9T's luminous aesthetic. The back glistens in the light giving off a captivating radiance. Pick your favorite from four stunning colors; Twilight Blue, Sunrise Orange, Ocean Green and Carbon Gray.",
                  Price = 3450m,
                  Image1Url = "~/img/mockImages/Mock1.1",
                  Image2Url = "~/img/mockImages/MockEmpty",
                  Image3Url = "~/img/mockImages/MockEmpty",
                  Category = _categoryRepository.GetCategoryByName("Phones"),
                  ProductDetails = new List<ProductDetail>()
                  {
                      new ProductDetail
                      {
                          Title = "Display Size",
                          Content = "5.45 inches, 76.7 cm2 (~72.7% screen-to-body ratio)"
                      },
                       new ProductDetail
                      {
                          Title = "Display Resolution",
                          Content = "480 x 960 pixels, 18:9 ratio (~197 ppi density)"
                      },
                        new ProductDetail
                      {
                          Title = "Display Type",
                          Content = "IPS LCD capacitive touchscreen, 16M colors"
                      }
                  },




                }
            };
            foreach (var category in categories)
            {
                _categoryRepository.AddCategory(category);
            }
            return _categoryRepository.SaveChanges();
        }


    }
}
