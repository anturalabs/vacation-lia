using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnturaSemester.Data;
using AnturaSemester.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnturaSemester.Controllers
{
    public class SemesterViewComponent : ViewComponent
    {
        private readonly UsersContext _context;

        public SemesterViewComponent(UsersContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var semester = new SemesterViewModel { };
            semester.users = _context.Users.ToList();
            semester.calendar = _context.Calendar.ToList();
            return View(semester);
            
        }











    }
}

