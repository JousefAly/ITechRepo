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
        private static int NumOfCategorySeeds = 0;
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
                int affectedRows = SeedCategoriesInDbManually();
                return affectedRows;
            }
            return 0;
        }
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

    }
}
