using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AnturaSemester.Data;

namespace AnturaSemester.Controllers
{
    public class CalendarViewComponent : ViewComponent
    {
        private readonly UsersContext _context;

        public CalendarViewComponent(UsersContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            {
                var users = _context.Users;
                ViewBag.caltest = users;
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
                /*ViewBag.Message = (days) + " days this month. I am a ViewBag practice message.";*/

                InitMonths();
                IncrMonth();


                return View(i);
            }
        }

        //GetDays()


        public IViewComponentResult Months;

        public void InitMonths()
        {
            List<DateTime> dateTimeMonths = new List<DateTime>
                    {
                        new DateTime(),
                    };

            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;


            {
                ViewBag.currentMonth = DateTime.Now.ToString("MMMM yyyy").ToUpper();

            }

        }

        public IViewComponentResult AddMonth;
        public void IncrMonth()
        {
            List<DateTime> timeMonths = new List<DateTime>
            {
                        new DateTime(),
                    };

            {
                ViewBag.timeMonths = System.DateTime.Now.AddMonths(+1).ToString("MMMM yyyy");


            }
        }
    }
}


/* return date.AddDays(1).AddMonths(1).AddDays(-1);*/
