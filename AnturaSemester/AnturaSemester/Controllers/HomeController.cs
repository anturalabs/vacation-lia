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
                return View(new EventViewModel());
            }

            public JsonResult GetEvents(DateTime start, DateTime end)
            {
                var viewModel = new EventViewModel();
                var events = new List<EventViewModel>();
                start = DateTime.Today.AddDays(-14);
                end = DateTime.Today.AddDays(-11);

                for (var i = 1; i <= 5; i++)
                {
                    events.Add(new EventViewModel()
                    {
                        id = i,
                        title = "Event " + i,
                        start = start.ToString(),
                        end = end.ToString(),
                        allDay = false
                    });

                    start = start.AddDays(7);
                    end = end.AddDays(7);
                }


                return Json(events.ToArray()); //, JsonRequestBehavior.AllowGet)
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
