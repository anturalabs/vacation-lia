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

        public IViewComponentResult Invoke(int year, int month, int prevMonth, int nextYear)
        {


            // IF som sätter år som nuvarande år om det har värde av noll. Samma för månad. 
            // IF pagination
            if (year > 0 && month == 0)
            {
                year = year - 1;
                month = 12;
            }

            if (year > 0 && month == 13)
            {
                year = year + 1;
                month = 1;
            }

            if (year == 0)
                year = DateTime.Now.Year;


            if (month == 0)
                month = DateTime.Now.Month;

            //today
            DateTime Today = DateTime.Today;




            //DAYS; amount of days in this month
            int daysInMonth = DateTime.DaysInMonth(year, month);

            //creates List for days in the current month
            List<CalendarDay> Darray = new List<CalendarDay>();
            // Loopar genom hela månad och retunerar helger (lördag & söndag) samt veckodagar.
            for (int i = 1; i <= daysInMonth; i++)
            {
                CalendarDay Day = new CalendarDay();
                
                Darray.Add(Day);

                DateTime iDay = new DateTime(year, month, i); // Highlight dagens datum - Dumma kod vill inte funka som den gjorde innan kraschen. 
                bool result = IsThisWeekend(iDay); // Kommer ej ihåg hur jag lyckades från början men klurar ut det sen.
                bool result1 = HighlightToday(DateTime.Today.Date);
                Day.weekDay = result;
                Day.highLight = result1;
                Day.Day = i;
                if (Day.weekDay == true)
                    if (Day.highLight == true)
                    {

                }
            }

            ViewBag.Column = Darray;

            ViewBag.currentMonth = new DateTime(year, month, 5).ToString("MMMM yyyy").ToUpper();
            ViewBag.year = year;
            ViewBag.today = Today;
            ViewBag.month = month;
            ViewBag.GetWeeks = GetWeeks;


            GetWeeks();
            GetHolidaysRedDays();

            //Retrieves saved holidays and returns them in calendar SH
            var calendar = new CalendarViewModel { };
            calendar.users = _context.Users.ToList();
            calendar.calendar = _context.Calendar.ToList();
            return View(calendar);




             int GetWeeks(DateTime time)
            {
                DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
                if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                {
                    time = time.AddDays(3);
                }

                return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            }


        }

        // ÖVRIGA RÖDA DAGAR - Röda dagar som förekommer under ett helt år utöver söndagar 
        HashSet<DateTime> GetHolidaysRedDays()
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


        bool HighlightToday(DateTime now)
        {
            if (DateTime.Today == now)
                return true;
            return false;
        }

        
    }
   
}












