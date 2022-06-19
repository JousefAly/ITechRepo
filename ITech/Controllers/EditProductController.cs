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
            return RedirectToAction(nameof(EditMainInformation), new { productId = product.Id });
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

            model.DetailToEdit.Content = model.DetailToEdit.Content.Replace("\r", " ").Replace("\n", " ");
            //add product relation without returning product to avoid tracking same entity twice by db.

            if (_productRepository.UpdateProductDetail(model.DetailToEdit) == null)
                TempData["StatusMessage"] = "Error : Detail was not updated";
            TempData["StatusMessage"] = "Detail updated successfully!";
            return RedirectToAction(nameof(EditDetails), new { productId = model.ProductId });
        }

        public IActionResult DeleteDetail(int detailId, int productId)
        {
            if (!_productRepository.DeleteProductDetail(detailId))
                TempData["StatusMessage"] = "Error : Product detail was not Deleted!";
            TempData["StatusMessage"] = "Detail was deleted Successfully!";
            return RedirectToAction(nameof(EditDetails), new { productId });
        }





        public IActionResult EditImages(int productId)
        {
            ViewData["productId"] = productId;
            var model = new EditProductImagesViewModel
            {
                ProductId = productId,
                ProductImages = _productRepository.GetById(productId).ProductImages,
            };
            if (TempData["StatusMessage"] != null)
                model.StatusMessage = TempData["StatusMessage"] as string;
            return View(model);
        }
        public IActionResult DeleteImage(int imageid, int productId)
        {
            var image = _productRepository.GetProductImage(imageid);            
            if(image.ImageNumber == 1)
            {
                TempData["StatusMessage"] = "Error: Cant delete main image if you want to delete image, assign another main image first.";
                return RedirectToAction(nameof(EditImages), new { productId });
            }
            if (!_productRepository.DeleteProductImage(imageid))
            {
                TempData["StatusMessage"] = "Error: Image was not deleted.";
                return RedirectToAction(nameof(EditImages), new { productId });
            }
            TempData["StatusMessage"] = "Image Deleted Successfully!";
            return RedirectToAction(nameof(EditImages), new { productId });
        }
        public IActionResult SetMainImage(int productId, int imageId)
        {
            var oldMainImage = _context.ProductImages
                                .FirstOrDefault(pi => pi.ProductId == productId && pi.ImageNumber == 1);
            var imageToBeMain = _context.ProductImages.Find(imageId);
            if(imageId == oldMainImage.Id)
            {
                TempData["StatusMessage"] = "Image is already the main image.";
                return RedirectToAction(nameof(EditImages), new { productId });
            }
            oldMainImage.ImageNumber = imageToBeMain.ImageNumber;
            imageToBeMain.ImageNumber = 1;
            if(_context.SaveChanges() == 0)
            {
                TempData["StatusMessage"] = "Error happened while updating main image in Database!";
                return RedirectToAction(nameof(EditImages), new { productId });
            }
            TempData["StatusMessage"] = "Changed Main Image Successfully!";
            return RedirectToAction(nameof(EditImages), new { productId });
        }
        [HttpGet]
        public IActionResult ManageStock(int productId)
        {
            ViewData["productId"] = productId;

            return View(_productRepository.GetById(productId));
        }
        [HttpPost]
        public IActionResult ManageStock(Product productModel)
        {
            var newStock = productModel.Stock;
            if(newStock < 0)
            {
                ViewData["productId"] = productModel.Id;
                TempData["StatusMessage"] = "Error: can't enter negative stock.";
                return RedirectToAction(nameof(ManageStock), new { productId = productModel.Id });
            }
            var oldStock = _productRepository.GetById(productModel.Id).Stock;
            var resultStock = 0;
            if(newStock == oldStock)
            {
                ViewData["productId"] = productModel.Id;
                TempData["StatusMessage"] = "Stock was not changed because it is the same.";
                return RedirectToAction(nameof(ManageStock), new { productId = productModel.Id });
            }
            else if( newStock > oldStock)
            {
                resultStock = _productRepository.AddToStock(productModel.Id, newStock - oldStock);                
            }
            else
            {
                resultStock = _productRepository.RemoveFromStock(productModel.Id, oldStock - newStock);
            }
            ViewData["productId"] = productModel.Id;
            TempData["StatusMessage"] = "Stock changed Successfully to " + resultStock;
            return RedirectToAction(nameof(ManageStock), new { productId = productModel.Id });
        }
    }
}
