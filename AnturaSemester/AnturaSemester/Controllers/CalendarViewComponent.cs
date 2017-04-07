using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

=======
using AnturaSemester.Data;
>>>>>>> 8d06bfe817492bead5fcc1857328503bc78f6160

namespace AnturaSemester.Controllers
{
    public class CalendarViewComponent : ViewComponent
    {
<<<<<<< HEAD
        public IViewComponentResult Invoke(int year, int month)

        {
            {

            }

            {
=======
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
>>>>>>> 8d06bfe817492bead5fcc1857328503bc78f6160
                DateTime Today = DateTime.Today;


                //DAYS; amount of days in this month
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);





                //creates List for days in the current month

                /* List<int> Darray = new List<int>();
                 int i = 0;
                 while (i < daysInMonth)
                 {
                     i++;
                     Darray.Add(i);
                 }


                 ViewBag.Column = Darray; */



                /*
                DateTime currentDate = DateTime.Now;
                int currentMonth = currentDate.Month;

                DateTime prevMonth = DateTime.Now;
                prevMonth = prevMonth.AddMonths(-1);

                DateTime nextMonth = DateTime.Now;
                nextMonth = nextMonth.AddMonths(+1);
                */


                //days in next month 
                /* int daysInNextMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++;

               List<int> DaysNextMonth = new List<int>();
                 int x = daysInMonth;
                 while (daysInMonth < daysInNextMonth)
                 {
                     x++;
                     DaysNextMonth.Add(i);


                 }*/



                /*
                 ViewBag.dateValue = Today;
                 ViewBag.DaysNextMonth = daysInNextMonth;
                 ViewBag.currentMonth = DateTime.Now.ToString("MMMM yyyy").ToUpper();
                 ViewBag.prevMonth = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy").ToUpper();   // Testing different methods for the browsing between months
                 ViewBag.nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM yyyy").ToUpper(); */
                ViewBag.Today = DateTime.Today;
                ViewBag.Message = (DateTime.Today) + " is weekday.";



                GetHolidays();
                

                return View();
                ;


            }
            HashSet<DateTime> GetHolidays()
            {
                HashSet<DateTime> holidays = new HashSet<DateTime>();
                HashSet<DateTime> weekends = new HashSet<DateTime>();

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

                /* var aweHelg = (from day in Enumerable.Range(1, 30) // En test röd-dag som alltid förekommer på den 3:e torsdag i november. 
                                where new DateTime(year/ 11/ day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
                 DateTime aweHelgDag = new DateTime(year/ 11/ aweHelg);
                 holidays.Add(aweHelgDag.Date); */

               /* foreach (DateTime DayOfWeek in GetHolidays()) ; */


                return holidays;

            }
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

                else if (holiday.DayOfWeek == DayOfWeek.Sunday)
                {
                    return holiday.AddDays(1);
                }
                else
                {

                    /* ViewBag.Saturday = DayOfWeek.Saturday;
                     ViewBag.MessageSaturday = (DayOfWeek.Saturday) + "THIS DAY!";
                     ViewBag.Sunday = DayOfWeek.Sunday;
                     ViewBag.MessageSunday = (DayOfWeek.Sunday) + "THIS DAY NEXT!";

                     foreach (DateTime DayOfWeek in GetHolidays()) ; */

    

                    return holiday;
                }


            }

  

            /*
             {
                 if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)  // IF-statement to write out weekends.

            {
                         return DayOfWeek.Saturday.ToString(

                 }
                 else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                     {
                     return holiday.AddDays(1);
                 }

                 else
                 {

                 }

                 return holiday;
             } */






            /*
            for (int x = 1; x <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); x++)
            {

                days += currentMonth;
            }
            */



            //test message remove later
            /*ViewBag.Message = (days) + " days this month. I am a ViewBag practice message.";*/



            /* InitMonths(); */
            /*  ModiMonth();*/


        }
        }
    }


        

        /*
                public IViewComponentResult Months;  // My code to view current and next month. 
                public void InitMonths(int currentMonth)
                {          
                    {
                        List<int> nextMonth = new List<int>();
                        int i = 0;
                        while (i < currentMonth)
                        {
                            i++;
                            nextMonth.Add(i);
                        }
                        ViewBag.nextMonth = nextMonth;
                        ViewBag.currentMonth = DateTime.Now.ToString("MMMM yyyy").ToUpper();

                        return (int nextMonth);


                } */






        /*    var startDate = DateTime.Now;
              var first = new DateTime(startDate.Year, startDate.Month, 1);

              List<DateTime> weekends = new List<DateTime>();

              for (int x = 0; x <= DateTime.DaysInMonth(startDate.Year, startDate.Month); x++)
              {
                  var nextDay = first.AddDays(x);

                  if (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
                  {
                      weekends.Add(nextDay);

                  ViewBag.Weekend = weekends;
                  }
              } */

        //GetDays()

        /*
                public IViewComponentResult Months;
                public int InitMonths;




            int CurrentMonth = DateTime.Now.Month;

             List<int> nextMonth = new List<int>();
             int i = 0;
             int currentMonth = 0;
             while (i < currentMonth)
             {
                 i++;
                 nextMonth.Add(i);
             }
             ViewBag.nextMonth = nextMonth;
             ViewBag.currentMonth = currentMonth;

             return (currentMonth);

         }
        */

        /*
                    DateTime currentDate = DateTime.Now;
                    int currentMonth = currentDate.Month;

                    DateTime prevMonth = DateTime.Now;
                    prevMonth = prevMonth.AddMonths(-1);

                    DateTime nextMonth = DateTime.Now;
                    nextMonth = nextMonth.AddMonths(+1);


                    {
                        ViewBag.currentMonth = DateTime.Now.ToString("MMMM yyyy").ToUpper();
                        ViewBag.prevMonth = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy").ToUpper();   // Testing different methods for the browsing between months
                        ViewBag.nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM yyyy").ToUpper();*/

        /*
                    }

                }


                public void ModiMonth() { }

                public DateTime NextMonth { get; private set; }
                public DateTime PrevMonth { get; private set; }

                void BtnNext_Click(object sender, EventArgs e)

                {
                    NextMonth = System.DateTime.Now.AddMonths(+1);
                    object lblDateCal = null;
                    lblDateCal = DateTime.Now.ToString("MMMM yyyy").ToUpper();
                }

                protected void BtnPrev_Click(object sender, EventArgs e)
                {
                    PrevMonth = System.DateTime.Now.AddMonths(-1);
                    object lblDateCal = null;
                    lblDateCal = DateTime.Now.ToString("MMMM yyyy").ToUpper();

                    ViewBag.prevMonth = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy").ToUpper();   // Testing different methods for the browsing between months
                    ViewBag.nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM yyyy").ToUpper();
                }
                */

        // Testing with buttons


        /* public IViewComponentResult AddMonth;
         public void IncrMonth()
         {

             List<DateTime> nextMonth = new List<DateTime>();           
             {
                 new DateTime();
             }; 


           ViewBag.nextMonth = DateTime.Now.AddMonths(+1).ToString("MMMM yyyy").ToUpper();
         }  */
    


/*
public int InitMonths
{
    get;
    [Display(currentDate = DateTime.Now.ToString)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
    public System.DateTime ForPeriod; { get; set; }
}


    */



/* ViewBag.timeMonths = timeMonths; */

/* return date.AddDays(1).AddMonths(1).AddDays(-1);*/



    /*List<string> MonthsNames = new List<string>
                    {
                        "Januari",
                        "Februari",
                        "Mars",
                        "April",
                        "Maj",
                        "Juni",
                        "Juli",
                        "Augusti",
                        "September",
                        "Oktober",
                        "November",
                        "December"
                    };
            var MonthNames = MonthsNames
           .Select(x => new { Name = x, Sort = DateTime.ParseExact(x, "MMMM", System.Globalization.CultureInfo.InvariantCulture) })
           .OrderBy(x => x.Sort.Month)
           .Select(x => x.Name)
           .ToArray();
           
        


        /* string[] MonthsNames1 =
        System.Globalization.CultureInfo.CurrentCulture
       .DateTimeFormat.MonthGenitiveNames;*/

        /*ViewData["MonthsNames"] = MonthsNames;

    return View();*/