using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnturaSemester.Controllers
{
    public class CalendarController : Controller
    {
        

        public IActionResult Index()
        {
            //today
            DateTime Today = DateTime.Today;

            //days this month
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            ViewBag.Message = (days) + "days this month";
    
            



            return View();
            //GetDays()
            //CurrentMonth
        }
    }
}