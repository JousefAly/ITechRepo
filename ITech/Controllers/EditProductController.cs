using ITech.Data;
using ITech.ViewModels;
using ITech.Data.Entites;
using ITech.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class EditProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;

        public EditProductController(IProductRepository productRepository,
                                     ICategoryRepository categoryRepository,
                                     ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }
        public IActionResult Index(int productId)
        {
            ViewData["productId"] = productId;
            return View(_productRepository.GetById(productId));
        }
        public IActionResult EditMainInformation(int productId)
        {
            ViewData["productId"] = productId;
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(_productRepository.GetById(productId));
        }
        [HttpPost]
        public IActionResult EditMainInformation(Product product, int categoryId)
        {
            //make sure to not track the old product
            var oldProduct = _productRepository.GetById(product.Id);
            _context.Entry(oldProduct).State = EntityState.Detached;

            product.LaunchTime = oldProduct.LaunchTime;
            product.Category = _categoryRepository.GetCategoryById(categoryId);
            _productRepository.Update(product);
            return RedirectToAction(nameof(EditMainInformation), new { productId = product.Id});
        }
        public IActionResult EditDetails(int productId, int detailId = 0)
        {
            ViewData["productId"] = productId;
            var details = _productRepository.GetById(productId).ProductDetails;

            var model = new EditProductDetailsViewModel
            {
                ProductDetails = details,
                ProductId = productId,
                
            };
            if (detailId != 0)
                model.DetailToEdit = details.FirstOrDefault(d => d.Id == detailId);
            if (TempData["StatusMessage"] != null)
                model.StatusMessage = TempData["StatusMessage"] as string;
            return View(model);
        }
        [HttpPost]
        public IActionResult EditDetail(EditProductDetailsViewModel model)
        {
            var editedDetail = model.DetailToEdit;
            editedDetail.Content = editedDetail.Content.Replace("\r", " ").Replace("\n", " ");
            editedDetail.Product = _productRepository.GetById(model.ProductId);
            _context.Entry(editedDetail).State = EntityState.Detached;
            if (_productRepository.UpdateProductDetail(editedDetail) == null)
                TempData["StatusMessage"] = "Error : Detail was not updated";
            TempData["StatusMessage"] = "Detail updated successfully!";
            return RedirectToAction(nameof(EditDetails), new { productId = model.ProductId });
        }







        public IActionResult EditImages(int productId)
        {
            ViewData["productId"] = productId;
            return View();
        }
    }
}
