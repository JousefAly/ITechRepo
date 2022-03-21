using ITech.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerRepository _sellerRepository;

        public SellerController(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }
        public IActionResult CompleteRegister()
        {
            return View();
        }
    }
}
