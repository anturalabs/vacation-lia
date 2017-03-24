using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnturaSemester.Controllers
{
    public class CalendarViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            {
                //today
                DateTime Today = DateTime.Today;


                //days this month
                int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                //creates List for days in the current month
                List<int> Darray = new List<int>();
                int i = 0;
                while (i < days)
                {
                    i++;
                    Darray.Add(i);
                }

                ViewBag.Column = Darray;

                //test message remove later
                ViewBag.Message = (days) + " days this month. I am a ViewBag practice message.";

                return View(i);

                //GetDays()
                //CurrentMonth
            }
        }
    }
}