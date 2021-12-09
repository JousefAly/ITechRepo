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
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;

        public Seeder(ICategoryRepository categoryRepository,
                        IProductRepository productRepository,
                        ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _context = context;
        }
        //this method seed products 
        //desiredSeed is which seed you want
        //return affected rows in Database
        public int SeedProducts(int desiredSeed)
        {
            if (desiredSeed <= 0)
            {
                //if no seeds already in db create the desired one for products
                var seed = _context.Seeds.FirstOrDefault(s => s.NameOfSeedType == "Products" && s.DesiredSeed == desiredSeed);
                if (seed == null)
                {
                    seed = new Seed();
                    seed.DesiredSeed = desiredSeed;
                    seed.NameOfSeedType = "Products";
                    seed.SeedAttempts = 1;
                    seed.Seeded = true;
                    _context.Seeds.Add(seed);
                    _context.SaveChanges();
                    return SeedProductsInDbManually();

                }
                seed.SeedAttempts += 1;
                _context.SaveChanges();
                return 0;
            }
            return 0;

        }

        //this method seed Categories 
        //desiredSeed is which seed you want
        //return affected rows in Database
        public int SeedCategories(int desiredSeed)
        {
            //if no seeds already in db create the desired one for categories
            var seed = _context.Seeds.FirstOrDefault(s => s.NameOfSeedType == "Categories" && s.DesiredSeed == desiredSeed);
            if (seed == null)
            {
                seed = new Seed();
                seed.DesiredSeed = desiredSeed;
                seed.NameOfSeedType = "Categories";
                seed.SeedAttempts = 1;
                seed.Seeded = true;
                _context.Seeds.Add(seed);
                _context.SaveChanges();
                return SeedCategoriesInDbManually();

            }


            seed.SeedAttempts += 1;
            _context.SaveChanges();
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
            var products = new List<Product>()
            {
                new Product
                {
                  Name = "Nokia C1 - 5.45-inch 16GB/1GB Dual SIM 3G Mobile Phone - Charcoal",
                  ShortDescription = "Level up to the new Nokia C1. Enjoy your entertainment – anytime, anywhere – with the large screen and all-day battery life. Raise your selfie game with the front-facing flash and 5 MP camera. Do more with Android 9 Pie (Go edition) – watch your favorite videos, view your photos and even find your way home all while offline.",
                  Price = 1059m,
                  Image1Name = "Mock1.1.jpg",
                  Image2Name = "MockEmpty.jpg",
                  Image3Name = "MockEmpty.jpg",
                  Image4Name = "MockEmpty.jpg",
                  Image5Name = "MockEmpty.jpg",
                  Image6Name = "MockEmpty.jpg",
                  Image7Name = "MockEmpty.jpg",
                  Image8Name = "MockEmpty.jpg",

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
                  }



                },
                new Product
                {
                  Name = "XIAOMI Redmi 9T - 6.53-inch 128GB/6GB Dual SIM Mobile Phone - Twilight Blue",
                  ShortDescription = "Brighten up your life with Redmi 9T's luminous aesthetic. The back glistens in the light giving off a captivating radiance. Pick your favorite from four stunning colors; Twilight Blue, Sunrise Orange, Ocean Green and Carbon Gray.",
                  Price = 3450m,
                  Image1Name = "Mock2.1.jpg",
                  Image2Name = "Mock2.2.jpg",
                  Image3Name = "Mock2.3.jpg",
                  Image4Name = "Mock2.4.jpg",
                  Image5Name = "Mock2.5.jpg",
                  Image6Name = "Mock2.6.jpg",
                  Image7Name = "Mock2.7.jpg",
                  Image8Name = "Mock2.8.jpg",
                  Category = _categoryRepository.GetCategoryByName("Phones"),
                  ProductDetails = new List<ProductDetail>()
                  {
                      new ProductDetail
                      {
                          Title = "Speed",
                          Content = "HSPA 42.2/5.76 Mbps, LTE-A"
                      },
                       new ProductDetail
                      {
                          Title = "Display",
                          Content = "1080 x 2340 pixels, 19.5:9 ratio (~395 ppi density)"
                      },
                        new ProductDetail
                      {
                          Title = "Main Camera",
                          Content = "Quad: 48 MP, f/1.8, 26mm (wide), 1/2.0, 0.8µm, PDAF"
                      }
                  }




                },
                 new Product
                {
                  Name = "Samsung Galaxy A12 - 6.5-inch 64GB/4GB Dual SIM Mobile Phone - Blue",
                  ShortDescription = "This is a very nice phone",
                  Price = 2777m,
                  Image1Name = "Mock3.1.jpg",
                  Image2Name = "Mock3.2.jpg",
                  Image3Name = "Mock3.3.jpg",
                  Image4Name = "Mock3.4.jpg",
                  Image5Name = "Mock3.5.jpg",
                  Image6Name = "MockEmpty.jpg",
                  Image7Name = "MockEmpty.jpg",
                  Image8Name = "MockEmpty.jpg",
                  Category = _categoryRepository.GetCategoryByName("Phones"),
                  ProductDetails = new List<ProductDetail>()
                  {
                      new ProductDetail
                      {
                          Title = "Speed",
                          Content = "HSPA 42.2/5.76 Mbps, LTE-A"
                      },
                       new ProductDetail
                      {
                          Title = "Display",
                          Content = "1080 x 2340 pixels, 19.5:9 ratio (~395 ppi density)"
                      },
                        new ProductDetail
                      {
                          Title = "Main Camera",
                          Content = "Quad: 48 MP, f/1.8, 26mm (wide), 1/2.0, 0.8µm, PDAF"
                      }
                  }




                },
                 new Product
                {
                  Name = "HP ProBook 450 G7 Laptop - Intel Core I7 - 8GB RAM - 1TB HDD - 15.6-inch HD - 2GB GPU - Windows 10 Pro - Natural Silver + Laptop Bag",
                  ShortDescription = "Full-featured, thin, and light, the reliable HP ProBook 450 offers essential commercial features at an affordable price to every business. Automatic security solutions, powerful performance, and long battery life help keep your business productive.",
                  Price = 15555m,
                  Image1Name = "Mock4.1.jpg",
                  Image2Name = "Mock4.2.jpg",
                  Image3Name = "Mock4.3.jpg",
                  Image4Name = "Mock4.4.jpg",
                  Image5Name = "Mock4.5.jpg",
                  Image6Name = "MockEmpty.jpg",
                  Image7Name = "MockEmpty.jpg",
                  Image8Name = "MockEmpty.jpg",
                  Category = _categoryRepository.GetCategoryByName("Laptops"),
                  ProductDetails = new List<ProductDetail>()
                  {
                      new ProductDetail
                      {
                          Title = "Processor",
                          Content = "Intel® Core™ i7-10510U processor with Intel® UHD Graphics 620 (1.8 GHz base frequency, up to 4.9 GHz with Intel® Turbo Boost Technology, 8 MB L3 cache, 4 cores)"
                      },
                       new ProductDetail
                      {
                          Title = "Memory",
                          Content = "8 GB DDR4-2400 SDRAM (1 x 8 GB)"
                      },
                        new ProductDetail
                      {
                          Title = "Graphics",
                          Content = "Discrete: NVIDIA® GeForce® MX130 (2 GB DDR5 dedicated)"
                      }
                  }




                },
                  new Product
                {
                  Name = "Lenovo IdeaPad L3 Laptop - Intel Core I7 - 8GB RAM - 1TB HDD + 256GB SSD - 15.6-inch FHD - 2GB GPU - DOS - Abyss Blue",
                  ShortDescription = "The new Lenovo™ IdeaPad™ L3 brings everything you need in an everyday-use laptop, but offers powerful memory and Intel® processing options, making it perfect for anyone who wants to do more than just surf the internet. Enjoy its FHD display and stereo speakers with your favorite streaming movies and shows and take it anywhere with a battery that lasts all day. Work, school, or home, the IdeaPad L3 has something for everyone.",
                  Price = 13760m,
                  Image1Name = "Mock5.1.jpg",
                  Image2Name = "Mock5.2.jpg",
                  Image3Name = "Mock5.3.jpg",
                  Image4Name = "Mock5.4.jpg",
                  Image5Name = "MockEmpty.jpg",
                  Image6Name = "MockEmpty.jpg",
                  Image7Name = "MockEmpty.jpg",
                  Image8Name = "MockEmpty.jpg",
                  Category = _categoryRepository.GetCategoryByName("Laptops"),
                  ProductDetails = new List<ProductDetail>()
                  {
                      new ProductDetail
                      {
                          Title = "Processor",
                          Content = "Intel® Core™ i7-10510U Processor, 1.80 GHz (8M Cache, up to 4.9 GHz, # of Cores: 4)"
                      },
                       new ProductDetail
                      {
                          Title = "Memory",
                          Content = "Installed Memory: 8 GB RAM"
                      },
                        new ProductDetail
                      {
                          Title = "Graphics",
                          Content = "Graphics Processor: NVIDIA® GeForce® MX330"
                      }
                  }

              }

            };
            foreach (var product in products)
            {
                _productRepository.AddProduct(product);
            }
            return _categoryRepository.SaveChanges();
        }


    }
}
