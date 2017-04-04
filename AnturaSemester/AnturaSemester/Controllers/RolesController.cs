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
        public async Task<IActionResult> Index()
        {
            var usersContext = _context.UserRole
                .Include(r => r.Users);

            return View(await usersContext.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.UserRole
                .Include(r => r.Users)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserRole,UserID")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "FirstName", roles.UserID);
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.UserRole.SingleOrDefaultAsync(m => m.ID == id);
            if (roles == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "FirstName", roles.UserID);
            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserRole,UserID")] Roles roles)
        {
            if (id != roles.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesExists(roles.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "FirstName", roles.UserID);
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.UserRole
                .Include(r => r.Users)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roles = await _context.UserRole.SingleOrDefaultAsync(m => m.ID == id);
            _context.UserRole.Remove(roles);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RolesExists(int id)
        {
            return _context.UserRole.Any(e => e.ID == id);
        }
    }
}
