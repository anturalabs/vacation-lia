﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnturaSemester.Models;
using Microsoft.AspNetCore.Mvc;
using AnturaSemester.Data;
using System.Data.Entity.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AnturaSemester.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersContext _context;

        public HomeController(UsersContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index(int year, int month)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            return View(_context.Users.ToList());

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbsence(string Absencetype, int UsersID, DateTime FromDate, DateTime ToDate)
        {
             //validation unique properties for absences (based on userID and date) so no overlapping in absences is allowed (it is possible right now)

            for (DateTime date = FromDate; date <= ToDate; date = date.AddDays(1))
            {
                
                var newcell = new CalendarCell { AbsenceName = Absencetype, UsersID = UsersID, Date = date };
                _context.Calendar.Add(newcell);
            }
            await _context.SaveChangesAsync();
            return Redirect("/");
        }







        public IActionResult Error()
        {
            return View();
        }



    }
}
