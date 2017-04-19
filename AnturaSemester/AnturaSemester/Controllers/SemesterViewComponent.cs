using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnturaSemester.Data;
using Microsoft.AspNetCore.Mvc;

namespace AnturaSemester.Controllers
{
    public class SemesterViewComponent
    {
        private readonly UsersContext _context;

        public SemesterViewComponent(UsersContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
