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
    public class RolesController : Controller
    {
        private readonly UsersContext _context;

        public RolesController(UsersContext context)
        {
            _context = context;
        }



        // GET: Roles
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["Rolerball"] = String.IsNullOrEmpty(sortOrder) ? "Roler" : "";




            var roles = from s in _context.Roles
                        select s;
            switch (sortOrder)
            {
                case "Roler":
                    roles = roles.OrderByDescending(s => s.RoleName);
                    break;

                default:
                    roles = roles.OrderBy(s => s.RoleName);
                    break;
            }
            int pageSize = 9;
            return View(await PaginatedList<Roles>.CreateAsync(roles.AsNoTracking(), page ?? 1, pageSize));

        }





        // GET: Roles/Create
        public IActionResult Create()
        {
            var roles = new Roles();

            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var roles = await _context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ID == id);

            if (roles == null)
            {
                return NotFound();
            }



            return View(roles);
        }






        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string RoleName)

        {
            if (id == null)
            {
                return NotFound();
            }

            var roleToUpdate = await _context.Roles
                .SingleOrDefaultAsync(s => s.ID == id);


            if (await TryUpdateModelAsync<Roles>(
           roleToUpdate,
           "",
           s => s.RoleName))
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



            return View(roleToUpdate);
        }










        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles



                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);


            if (roles == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roles = await _context.Roles
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (roles == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Roles.Remove(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

    }
}
