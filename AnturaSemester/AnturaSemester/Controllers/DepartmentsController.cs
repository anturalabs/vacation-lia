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
    public class DepartmentsController : Controller
    {
        private readonly UsersContext _context;

        public DepartmentsController(UsersContext context)
        {
            _context = context;    
        }

        // GET: Departments
        public async Task<IActionResult> Index(
           string sortOrder,
           int? page)
        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["Depo"] = String.IsNullOrEmpty(sortOrder) ? "Derp" : "";




            var departments = from s in _context.Department
                        select s;
            switch (sortOrder)
            {
                case "Derp":
                    departments = departments.OrderByDescending(s => s.DepartmentName);
                    break;

                default:
                    departments = departments.OrderBy(s => s.DepartmentName);
                    break;
            }
            int pageSize = 9;
            return View(await PaginatedList<Department>.CreateAsync(departments.AsNoTracking(), page ?? 1, pageSize));

        }

        

        // GET: Departments/Create
        public IActionResult Create()
        {
            var departments = new Department();
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.SingleOrDefaultAsync(m => m.ID == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DepartmentName")] Department department)
        {
            if (id != department.ID)
            {
                return NotFound();
            }

            var departmentToUpdate = await _context.Department
               .SingleOrDefaultAsync(s => s.ID == id);


            if (await TryUpdateModelAsync<Department>(
           departmentToUpdate,
           "",
           s => s.DepartmentName))
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



            return View(departmentToUpdate);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .SingleOrDefaultAsync(m => m.ID == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.SingleOrDefaultAsync(m => m.ID == id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.ID == id);
        }
    }
}
