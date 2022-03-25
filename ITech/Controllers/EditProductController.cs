using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class EditProductController : Controller
    {
        public IActionResult EditMainInformation(int productId)
        {
            return View();
        }
        public IActionResult EditDetails(int productId)
        {
            return View();
        }
        public IActionResult EditImages(int productId)
        {
            return View();
        }
    }
}
