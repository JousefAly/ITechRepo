using ITech.Data;
using ITech.Data.Entites;
using ITech.Data.Repositories;
using ITech.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ICategoryRepository _categoryRepository1;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(IProductRepository productRepository,
                                  ICategoryRepository categoryRepository,
                                  ISellerRepository sellerRepository,                                  
                                  UserManager<AppUser> userManager,
                                  IWebHostEnvironment hostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _sellerRepository = sellerRepository;                        
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        //return all products   
        public IActionResult Index(string category = "")
        {
            if(!string.IsNullOrEmpty(category))
            {
                return View(_productRepository.GetProductsByCategory(category, includeDetails: true).ToList());
            }
            return View(_productRepository.GetAllProducts());
        }
       
        public IActionResult Detail(int id)
        {
            var model = new ProductDetailViewModel
            {
                Product = _productRepository.GetById(id)
            };
            return View(model);
        }
        public IActionResult ManageProduct(int id)
        {
            return View(_productRepository.GetById(id));
        }      
        public IActionResult CreateProduct()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, int categoryId)
        {
            var appUser = await _userManager.GetUserAsync(HttpContext.User);

            var seller = _sellerRepository.GetUserSeller(appUser);
            product.Category = _categoryRepository.GetCategoryById(categoryId);
            var createdProduct = _productRepository.AddSellerProduct(seller, product);
            if (createdProduct == null)
                return BadRequest("Product was not created.");

            return RedirectToAction(nameof(CreateProductImages), new { productId = createdProduct.Id });
        }
        public IActionResult CreateProductImages(int productId)
        {
            var product = _productRepository.GetById(productId);
            return View(product);
        }
        public IActionResult UploadProductImage(int productId)
        {


            var model = new UploadProductImageViewModel
            {
                ProductId = productId,
                HasMainImage = _productRepository.HasMainImage(productId)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadProductImage(UploadProductImageViewModel model)
        {

            var addedImage = await _productRepository
                                      .AddProductImage(model.ImageFile, model.ImageNumber, model.ProductId);
            if (addedImage == null)
                return BadRequest("Image was not added to Database.");

            return RedirectToAction(nameof(UploadProductImage), new { productId = model.ProductId });
        }
        public IActionResult CancelUploadImage(int productId)
        {
            if (!_productRepository.HasMainImage(productId))
            {
                if (_productRepository.Delete(productId))
                {
                    TempData["successMessage"] = "Product Deleted Successfully";
                    return View();
                }
                TempData["errorMessage"] = "Product was not Deleted. Database Error.";
                return View();
            }

            return View();

        }
        public IActionResult AddProductDetail(int productId)
        {
            var model = new AddProductDetailViewModel
            {
                ProductId = productId,
                Title = string.Empty,
                Content = string.Empty,
                ProductDetails = _productRepository.GetById(productId).ProductDetails
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddProductDetail(AddProductDetailViewModel model)
        {
            var product = _productRepository.GetById(model.ProductId);
            model.Content = model.Content.Replace("\n", " ").Replace("\r", " ");
            var detail = new ProductDetail
            {
                Title = model.Title,
                Content = model.Content,
            };
            var addedDetail = _productRepository.AddProductDetail(product, detail);
            if (addedDetail == null)
            {
                TempData["isDetailAdded"] = false;
                return RedirectToAction(nameof(AddProductDetail), new { productId = model.ProductId });
            }
            TempData["isDetailAdded"] = true;
            return RedirectToAction(nameof(AddProductDetail), new { productId = model.ProductId });
        }
        public IActionResult DeleteProductImage(int imageId)
        {
            var productId = _productRepository.GetProductImage(imageId).ProductId;
            if (!_productRepository.DeleteProductImage(imageId))
                TempData["StatusMessage"] = " Error: Image was not deleted"; 
            TempData["StatusMessage"] = "Image Deleted Successfully";
            return RedirectToAction(nameof(ManageProduct), new { id = productId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (!_productRepository.Delete(id))
            {
                TempData["StatusMessage"] = "Error: Product was not deleted.";
            }
            TempData["StatusMessage"] = "Product was deleted successfully.";
            return RedirectToAction(nameof(ManageProducts));
        }
        [Authorize(Roles ="Admin")]
        public IActionResult ManageProducts()
        {

            return View(_productRepository.GetAllProducts());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ActivateProduct(int id)
        {
            if(!_productRepository.Activate(id))
            {
                TempData["StatusMessage"] = "Error: Product: " + id + " is not Activated.";
                return View("ManageProducts", _productRepository.GetAllProducts());
            }
            TempData["StatusMessage"] = "Product: " + id + " is Activated.";
            return View("ManageProducts",_productRepository.GetAllProducts());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeactivateProduct(int id)
        {
            if (!_productRepository.DesActivate(id))
            {
                TempData["StatusMessage"] = "Error: Product: " + id + " is not Dectivated.";
                return View("ManageProducts", _productRepository.GetAllProducts());
            }
            TempData["StatusMessage"] = "Product: " + id + " is Dectivated.";
            return View("ManageProducts", _productRepository.GetAllProducts());

        }

        public ViewResult ProductStats(int id)
        {            
            return View(_productRepository.GetProductStats(id));
        }
        [HttpPost]
        public IActionResult SearchProducts(string searchString)
        {
            searchString ??= "";
            return View(_productRepository.Search(searchString));
        }
        public IActionResult TopSellingProducts(int numOfProducts)
        {
            
            var products = _productRepository.GetTopSellingProducts(numOfProducts);
            return Ok();
        }
    }
}
