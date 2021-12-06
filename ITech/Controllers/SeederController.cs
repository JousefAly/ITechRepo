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
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;

        public SeederController(ICategoryRepository categoryRepository,
                                IProductRepository productRepository,
                                ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _context = context;
        }
        public IActionResult SeedCategories(int desiredSeed)
        {
            Seeder seeder = new Seeder(_categoryRepository, _productRepository, _context);
            int rowsAffected = seeder.SeedCategories(desiredSeed);
            ViewBag.RowsAffected = rowsAffected.ToString();
            return View();
        }
        public IActionResult SeedProducts(int desiredSeed)
        {
            Seeder seeder = new Seeder(_categoryRepository, _productRepository, _context);
            int rowsAffected = seeder.SeedProducts(desiredSeed);
            ViewBag.RowsAffected = rowsAffected.ToString();
            return View();
        }
    }
}
