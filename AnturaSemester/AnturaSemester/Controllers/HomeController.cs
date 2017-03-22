using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnturaSemester.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnturaSemester.Controllers
{
    public class HomeController : Controller
    {
        
            [HttpGet]
            public ActionResult Index()
            {
                return View();
            }

           
        
    
    public IActionResult Users()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        

       
    }
}
