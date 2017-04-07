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

                

                InitMonths();
                IncrMonth();

              //  var TestMonth = DateTime.Now.Month;
              //  var MinusMonth= TestMonth.AddDays(-1)



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


       /*  MyDate()
        {
            DateTime today = DateTime.Now;
            this.Year = today.Year;
            this.Month = today.Month;
        }

        public static void Main()
        {
            MyDate oCalendar = new MyDate();
            oCalendar.ShowCalendar();
            oCalendar.ShowMenu();
        }

        public String GetMonthName(int Month, int MonthFormat)
        {
            //Delcaring MonthNames in the Array
            String[] MonthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (MonthFormat == 0)
            {
                return MonthNames[Month - 1].Substring(0, 3);
            }
            if (MonthFormat == 1)
            {
                return MonthNames[Month - 1];
            }
            if (MonthFormat != 0 && MonthFormat != 1)
            {
                return "Invalid Month Index";
            }
            return "Invalid Month Index";
        }

        public void ShowMenu()
        {
            bool CanExit = false;
            String strChoice;
            do
            {
                Console.WriteLine("Calendar Menu");
                Console.WriteLine("=================");
                Console.WriteLine("1. [> ] Next Month");
                Console.WriteLine("2. [< ] Prev Month");
                Console.WriteLine("3. [>>] Next Year");
                Console.WriteLine("4. [<<] Prev Year");
                Console.WriteLine("5. [x ] Exit");
                Console.Write("Enter Your Choice: ");
                strChoice = Console.ReadLine();
                switch (strChoice.ToLower())
                {
                    case ">":
                        if (Month == 12)
                        {
                            Year += 1;
                            Month = 1;
                        }
                        else
                        {
                            Month += 1;
                        }
                        break;
                    case "<":
                        if (Month == 1)
                        {
                            Month = 12;
                            Year -= 1;
                        }
                        else
                        {
                            Month -= 1;
                        }
                        break;
                    case ">>":
                        Year += 1;
                        break;
                    case "<<":
                        Year -= 1;
                        break;
                    case "x":
                        CanExit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
                if (!CanExit)
                    ShowCalendar();
            }
            while (!CanExit);
        }
        public void ShowCalendar()
        {
            int[] arrDays = new int[35];
            int DayIndex;
            DateTime objDate = new DateTime(Year, Month, Day);

            //Get the In weekday the first of month falls
            int StartDayIndex = (int)objDate.DayOfWeek;

            Console.WriteLine("{0} - {1}", GetMonthName(Month, 1), Year);
            for (int i = 0; i < 35; i++)
            {
                DayIndex = (StartDayIndex + i) % 35;
                if (i < DateTime.DaysInMonth(Year, Month))
                {
                    arrDays[DayIndex] = (i + 1);
                }
                else
                {
                    arrDays[DayIndex] = 0;
                }
            }
            Console.WriteLine(new String('*', 64));
            Console.WriteLine("\tSu\tMo\tTu\tWe\tTh\tFr\tSa");
            Console.WriteLine(new String('*', 64));

            for (int i = 0; i < 5; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < 7; j++)
                {
                    if (arrDays[7 * i + j] > 0)
                    {
                        Console.Write("{0,-2}\t", arrDays[7 * i + j]);
                    }
                    else
                    {
                        Console.Write("{0,-2}\t", " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(new String('*', 64));
*/

        }
}



/* return date.AddDays(1).AddMonths(1).AddDays(-1);*/
