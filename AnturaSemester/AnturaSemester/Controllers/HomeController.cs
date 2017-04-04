using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnturaSemester.Models;
using Microsoft.AspNetCore.Mvc;
using AnturaSemester.Data;


namespace AnturaSemester.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersContext _context;

        public HomeController(UsersContext context)
        {
            _context = context;

        }

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



        public IActionResult Error()
        {
            return View();
        }



    }
}
