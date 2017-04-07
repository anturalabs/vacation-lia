using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnturaSemester.Data;
using AnturaSemester.Models;

namespace AnturaSemester.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }



        // GET: Users
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RoleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Role" : "";
            ViewData["DepartmentSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Department" : "";



            var users = from s in _context.Users
                        select s;
            var roles = from rs in _context.Users
                        select rs;



            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.LastName);
                    break;
                case "Role":
                    roles = roles.OrderByDescending(s => s.UsersRole);
                    break;
                //case "Department":
                   // users = users.OrderByDescending(s => s.UsersDepartment);
                   // break;
                default:
                    users = users.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Users>.CreateAsync(users.Include(r => r.UsersRole).ThenInclude(e => e.Role).AsNoTracking(), page ?? 1, pageSize));
            //var users = await _context.Users
            // .Include(r => r.UsersRole)
            // .ThenInclude(e => e.Role)
            //     // .Include(d => d.UsersDepartment)
            //  .AsNoTracking()
            //  .SingleOrDefaultAsync(m => m.ID == id);
        }



        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(r => r.UsersRole)
                 .ThenInclude(e => e.Role)
                // .Include(d => d.UsersDepartment)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);



            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,Role,Department")] Users users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(users);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Users>(
                userToUpdate,
                "",
                s => s.FirstName, s => s.LastName, s => s.UsersRole)) //, s => s.UsersDepartment
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
            return View(userToUpdate);
        }







        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Users.Remove(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }



        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
