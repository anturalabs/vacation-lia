using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using AnturaSemester.Data;
using AnturaSemester.Models;

namespace AnturaSemester.Controllers
{
    public class CalendarViewComponent : ViewComponent
    {
     private readonly UsersContext _context;

        public CalendarViewComponent(UsersContext context)
        {
            _context = context;
        }
        
        public IViewComponentResult Invoke(int year, int month)

        {
            {
                //today
                DateTime Today = DateTime.Today;


                //DAYS; amount of days in this month
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                //creates List for days in the current month
                List<CalendarDay> Darray = new List<CalendarDay>();
                // Loopar genom hela månad och retunerar helger (lördag & söndag) samt veckodagar.
                for (int i = 1; i <= daysInMonth; i++)
                {
                    CalendarDay Day = new CalendarDay();
                    Darray.Add(Day);

                    DateTime iDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                    bool result = IsThisWeekend(iDay);
                    Day.weekDay = result;
                    Day.Day = i;
                    if (Day.weekDay == true)

                    {
                       
                    }               
                }

                ViewBag.Column = Darray;



                
                /*
                 ViewBag.dateValue = Today;
                 ViewBag.DaysNextMonth = daysInNextMonth;
                 ViewBag.currentMonth = DateTime.Now.ToString("MMMM yyyy").ToUpper();
                 ViewBag.prevMonth = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy").ToUpper();   // Testing different methods for the browsing between months
                 ViewBag.nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM yyyy").ToUpper(); */
                ViewBag.currentMonth = DateTime.Now.ToString("MMMM yyyy").ToUpper();

                //Retrieves saved holidays and returns them in calendar
                GetHolidays();
                var calendar = new CalendarViewModel { };
                calendar.users = _context.Users.ToList();
                calendar.calendar = _context.Calendar.ToList();
                return View(calendar);

            }

            // ÖVRIGA RÖDA DAGAR - Röda dagar som förekommer under ett helt år utöver söndagar 
            HashSet<DateTime> GetHolidays()
            {

                HashSet<DateTime> holidays = new HashSet<DateTime>();


                DateTime newYearsDate = AdjustForWeekendHoliday(new DateTime(DateTime.Now.Year, 1, 1).Date); // Skriver ut nyår, alltid förekommer 1:a jan
                holidays.Add(newYearsDate);


                DateTime valborgEve = AdjustForWeekendHoliday(new DateTime(DateTime.Now.Year, 5, 1).Date); // skriver ut Valborg, alltid förekommer 1:a maj
                holidays.Add(valborgEve);

                DateTime nationalDay = AdjustForWeekendHoliday(new DateTime(DateTime.Now.Year, 6, 6).Date); // National dagen, alltid förekommer 6:e juni
                holidays.Add(nationalDay);

                DateTime christmasDay = AdjustForWeekendHoliday(new DateTime(DateTime.Now.Year, 12, 25).Date); // Juldagen, alltid förekommer 25:e dec
                holidays.Add(christmasDay);

                DateTime secondDayChristmas = AdjustForWeekendHoliday(new DateTime(DateTime.Now.Year, 12, 26).Date); // Annandagsjul, alltid förekommer 26:e dec
                holidays.Add(secondDayChristmas);

                /* var sofiaHelg = (from day in Enumerable.Range(1, 30) // En test röd-dag som alltid förekommer på den 3:e torsdag i november. 
                                where new DateTime(year/ 11/ day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
                 DateTime aweHelgDag = new DateTime(year/ 11/ sofiaHelg);
                 holidays.Add(sofiaHelgDag.Date); */

                /* foreach (DateTime DayOfWeek in GetHolidays()) ; */


                return holidays;

            }



            // Hänger ihop med ÖVRIGA RÖDA DAGAR (holidays)
            DateTime AdjustForWeekendHoliday(DateTime holiday)

            {

                if (holiday.DayOfWeek == DayOfWeek.Saturday)

                {
                    return holiday.AddDays(-1);

                }
                else if (holiday.DayOfWeek == DayOfWeek.Sunday)
                {
                    return holiday.AddDays(1);
                }

                else
                {

                    return holiday;
                }
            }

            // Retunerar helg (lördag & söndag) om resultat är true
             bool IsThisWeekend(DateTime now)
            {
                if (now.DayOfWeek == DayOfWeek.Saturday)
                    return true;
                if (now.DayOfWeek == DayOfWeek.Sunday)
                    return true;
                return false;

            }

        }

    }
}         


      





           
        

