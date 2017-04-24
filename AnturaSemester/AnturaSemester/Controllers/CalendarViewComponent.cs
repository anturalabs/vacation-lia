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


            // IF som s�tter �r som nuvarande �r om det har v�rde av noll. Samma f�r m�nad. 
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
            // Loopar genom hela m�nad och retunerar helger (l�rdag & s�ndag) samt veckodagar.
            for (int i = 1; i <= daysInMonth; i++)
            {
                CalendarDay Day = new CalendarDay();

                Darray.Add(Day);

                DateTime iDay = new DateTime(year, month, i); // Highlight dagens datum - Dumma kod vill inte funka som den gjorde innan kraschen. 
                bool result = IsThisWeekend(iDay); // Kommer ej ih�g hur jag lyckades fr�n b�rjan men klurar ut det sen.
                bool result1 = HighlightToday(iDay);
                Day.weekEnd = result;
                Day.highLight = result1;
                int GetWeekNum = culture.Calendar.GetWeekOfYear(iDay, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                Day.weekNumber = GetWeekNum;
                bool result2 = MondayTest(iDay);
                Day.getMonday = result2;
                //  int result3 = GetHolidaysRedDays.result3(iDay);
                // Day.holidaysRedDay = result3;
                Day.Day = i;


            }

            ViewBag.Column = Darray;

            ViewBag.currentMonth = new DateTime(year, month, 5).ToString("MMMM yyyy").ToUpper();
            ViewBag.year = year;
            ViewBag.month = month;



            //Retrieves saved holidays and returns them in calendar SH
            var calendar = new CalendarViewModel { };
            calendar.users = _context.Users.ToList();
            calendar.calendar = _context.Calendar.ToList();
            return View(calendar);


            DateTime GetHolidaysRedDays()
            {

                List<DateTime> holidays = new List<DateTime>();


                DateTime newYearsDate = (new DateTime(DateTime.Now.Year, 1, 1).Date); // Skriver ut ny�r, alltid f�rekommer 1:a jan
                holidays.Add(newYearsDate);


                DateTime valborgEve = (new DateTime(DateTime.Now.Year, 5, 1).Date); // skriver ut Valborg, alltid f�rekommer 1:a maj
                holidays.Add(valborgEve);

                DateTime nationalDay = (new DateTime(DateTime.Now.Year, 6, 6).Date); // National dagen, alltid f�rekommer 6:e juni
                holidays.Add(nationalDay);

                DateTime christmasDay = (new DateTime(DateTime.Now.Year, 12, 25).Date); // Juldagen, alltid f�rekommer 25:e dec
                holidays.Add(christmasDay);

                DateTime secondDayChristmas = (new DateTime(DateTime.Now.Year, 12, 26).Date); // Annandagsjul, alltid f�rekommer 26:e dec
                holidays.Add(secondDayChristmas);

                return GetHolidaysRedDays();
            }

        }

        // �VRIGA R�DA DAGAR - R�da dagar som f�rekommer under ett helt �r ut�ver s�ndagar 
        /*  HashSet<DateTime> GetHolidaysRedDays()
          {

              HashSet<DateTime> holidays = new HashSet<DateTime>(); */




        /* // H�nger ihop med �VRIGA R�DA DAGAR (holidays)
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
         } */

        // Retunerar helg (l�rdag & s�ndag) om resultat �r true
        bool IsThisWeekend(DateTime now)
        {
            if (now.DayOfWeek == DayOfWeek.Saturday)
                return true;
            if (now.DayOfWeek == DayOfWeek.Sunday)
                return true;
            // if (now.DayOfWeek == DateTime.Now.Year(1, 1))
            // return true
            return false;

        }
        bool MondayTest(DateTime now) // Checks every monday 
        {
            if (now.DayOfWeek == DayOfWeek.Monday)
                return true;
            return false;
        }



        bool HighlightToday(DateTime now)
        {
            if (DateTime.Today == now)
                return true;
            return false;
        }

        /* bool StartOfWeek(DateTime week)
         {

             if (StartOfWeek.week)
                 return true;
             return false;
         } */

    }

}
