using ITech.Data;
using ITech.Data.Entites;
using ITech.Data.Repositories;
using ITech.ViewModels;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(IProductRepository productRepository,
                                  ISellerRepository sellerRepository,
                                  ICategoryRepository categoryRepository,
                                  UserManager<AppUser> userManager,
                                  IWebHostEnvironment hostEnvironment)
        {
            _productRepository = productRepository;
            _sellerRepository = sellerRepository;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        //return all products   
        public IActionResult Index()
        {

            return View(_productRepository.GetAllProducts());
        }
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);

            return View(product);
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
            //upload image to wwwroot/img/products then connect image with its product
            //change the image name to unique name with productId attached to it
            var imageFile = model.ImageFile;
            string wwwrootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string extension = Path.GetExtension(imageFile.FileName);
            string imageUniqueName = fileName + DateTime.Now.ToString("yymmssfff") + "-"
                                     + model.ProductId.ToString() + extension;
            var path = Path.Combine(wwwrootPath + "/img/products/", imageUniqueName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            //now image uploaded connect it with product in db
            var product = _productRepository.GetById(model.ProductId);
            var productImage = new ProductImage
            {
                ImageNumber = model.ImageNumber,
                ImageUrl = "img/products/" + imageUniqueName,
                Product = product
            };
            var addedImage = _productRepository.AddProductImage(product, productImage);
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
                return RedirectToAction(nameof(AddProductDetail), new { productId = model.ProductId});
            }
            TempData["isDetailAdded"] = true;
            return RedirectToAction(nameof(AddProductDetail), new { productId = model.ProductId });
        }
        public IActionResult Edit(int productId)
        {
            return View(_productRepository.GetById(productId));
        }


    }
}
