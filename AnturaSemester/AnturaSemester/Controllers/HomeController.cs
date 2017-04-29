using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnturaSemester.Models;
using Microsoft.AspNetCore.Mvc;
using AnturaSemester.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> CreateAbsence(Guid guid, string Absencetype, int UsersID, DateTime FromDate, DateTime ToDate)
        {
            if (guid != null) {
                await DeleteAbsence(guid);
            }

            guid = Guid.NewGuid();
            for (DateTime date = FromDate; date <= ToDate; date = date.AddDays(1))
            {
                var newcell = new CalendarCell { ID = guid, AbsenceName = Absencetype, UsersID = UsersID, Date = date };

                if (_context.Calendar.ToList().Any(item => (item.Date == newcell.Date && item.UsersID == newcell.UsersID)))
                {
                    //modal and/or method needs to check and stop if error is true
                    ViewBag.error = "Not allowed, try again!";

                }
                else
                {
                    _context.Calendar.Add(newcell);
                }
            }
            await _context.SaveChangesAsync();


            return RedirectToAction("/");


        }



        public JsonResult EditAbsence(Guid id)
        {


            if (id != null)
            {
                var cell =  _context.Calendar.Where(m => m.ID == id).ToList();

                if (cell == null)
                {
              //      return NotFound();
                }
                return Json(cell);
            }
            return null;
        }

        [HttpPost, ActionName("EditAbsence")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbsence(Guid? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceToUpdate = await _context.Calendar
                .Include(r => r.Date)
                .Include(r => r.UsersID)

                .SingleOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<CalendarCell>(
                absenceToUpdate,
                "",
                s => s.UsersID, s => s.Date, s => s.AbsenceName, s => s.Approval, s => s.CommentField))
            {

                

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(absenceToUpdate);
        }
        

        // POST: Users/Delete/5
        [HttpPost, ActionName("DeleteAbsence")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAbsence(Guid id)
        {
            var calendar = await _context.Calendar
               .Include(r => r.Date)
                .Include(d => d.UsersID)
                .Include(t => t.AbsenceName)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (calendar == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Calendar.Remove(calendar);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
