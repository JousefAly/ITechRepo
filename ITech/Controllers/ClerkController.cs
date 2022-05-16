using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Controllers
{
    public class ClerkController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
