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


            //DAYS; amount of days in this month
            int daysInMonth = DateTime.DaysInMonth(year, month);
            var culture = new CultureInfo("sv-SE");

            //creates List for days in the current month
            List<CalendarDay> Darray = new List<CalendarDay>();
            // Loopar genom hela månad och retunerar helger (lördag & söndag) samt veckodagar.
            for (int i = 1; i <= daysInMonth; i++)
            {
                CalendarDay Day = new CalendarDay();

                Darray.Add(Day);

                DateTime iDay = new DateTime(year, month, i); // Highlight dagens datum - Dumma kod vill inte funka som den gjorde innan kraschen. 
                bool result = IsThisWeekend(iDay); // Kommer ej ihåg hur jag lyckades från början men klurar ut det sen.
                bool result1 = HighlightToday(iDay);
                Day.weekEnd = result;
                Day.highLight = result1;
                Day.Month = month;
                int GetWeekNum = GetIso8601WeekOfYear(iDay);
                Day.weekNumber = GetWeekNum;
                bool result2 = MondayTest(iDay);
                Day.getMonday = result2;
                Day.Day = i;


            }

            ViewBag.Column = Darray;

            ViewBag.currentMonth = new DateTime(year, month, 5).ToString("MMMM yyyy").ToUpper();
            ViewBag.year = year;
            ViewBag.month = month;

            ViewBag.GroupedWeeks = Darray.GroupBy(day => day.weekNumber).Select( group => new Tuple<int,int>(group.Key, group.Count())); // LINQ contains funktion för att ta fram veckonummer och skapa en lista av dem. 

            //Retrieves saved holidays and returns them in calendar SH
            var calendar = new CalendarViewModel { };
            calendar.users = _context.Users.ToList();
            calendar.calendar = _context.Calendar.ToList();
            return View(calendar);
       
        }


        // Retunerar helg (lördag & söndag) om resultat är true
        bool IsThisWeekend(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return true;
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return true;
           // if (date.Date == (new DateTime(date.Year, 5, 1).Date)) // bortkommentera - spara lösning inför framtiden
           //     return true;
            if (GetHolidaysRedDays(date.Year).Any(item => item.Date == date.Date)) // LINQ contains funktion som kollar igenom röda dagar i GetHolidaysRedDays list
                return true;
            return false;

        }
        bool MondayTest(DateTime now) // Checks every monday 
        {
            if (now.DayOfWeek == DayOfWeek.Monday)
                return true;
            return false;
        }

        bool HighlightToday(DateTime now) // För highlightning av dagen
        {
            if (DateTime.Today == now)
                return true;
            return false;
        }

        List<DateTime> GetHolidaysRedDays(int year) // Lista med "alla" röda dagar som förekommer under ett år och som är på samma datum
        {

            List<DateTime> holidays = new List<DateTime>();

            DateTime newYearsDate = (new DateTime(year, 1, 1).Date); // Skriver ut nyår, alltid förekommer 1:a jan
            holidays.Add(newYearsDate);


            DateTime valborgEve = (new DateTime(year, 5, 1).Date); // skriver ut Valborg, alltid förekommer 1:a maj
            holidays.Add(valborgEve);

            DateTime nationalDay = (new DateTime(year, 6, 6).Date); // National dagen, alltid förekommer 6:e juni
            holidays.Add(nationalDay);

            DateTime christmasDay = (new DateTime(year, 12, 25).Date); // Juldagen, alltid förekommer 25:e dec
            holidays.Add(christmasDay);

            DateTime secondDayChristmas = (new DateTime(year, 12, 26).Date); // Annandagsjul, alltid förekommer 26:e dec
            holidays.Add(secondDayChristmas);

            return holidays;
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }

}
