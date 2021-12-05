using ITech.Data;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class SeederController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public SeederController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult SeedCategories(int desiredSeed)
        {
            Seeder seeder = new Seeder(_categoryRepository);
            int rowsAffected = seeder.SeedCategories(desiredSeed);
            ViewBag.RowsAffected = rowsAffected.ToString();
            return View();
        }
    }
}
