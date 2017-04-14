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
            ViewData["FNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fname_asc" : "";
            ViewData["RoleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Role" : "";
            ViewData["DepartmentSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Department" : "";



            var users = from s in _context.Users
                        select s;
            var roles = from rs in _context.Users
                        select rs;
            var departments = from dp in _context.Users
                              select dp;



            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.LastName);
                    break;
                case "fname_asc":
                    users = users.OrderBy(s => s.FirstName);
                    break;
                case "Role":
                    roles = roles.OrderBy(s => s.UsersRole);
                    break;
                case "Department":
                    departments = users.OrderBy(s => s.UsersDepartment);
                    break;
                default:
                    users = users.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Users>.CreateAsync(users.Include(r => r.UsersRole).ThenInclude(e => e.Role).Include(f => f.UsersDepartment).ThenInclude(d => d.Departments).AsNoTracking(), page ?? 1, pageSize));

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
                .Include(d => d.UsersDepartment)
                 .ThenInclude(u => u.Departments)
                .Include(t => t.UsersTeam)
                 .ThenInclude(n => n.Teams)
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
        public async Task<IActionResult> Create([Bind("LastName,FirstName,Role,Department,Team")] Users users, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Add(users);
                    var user = await _context.Users
                .Include(r => r.UsersRole)
                 .ThenInclude(e => e.Role)
                .Include(d => d.UsersDepartment)
                 .ThenInclude(u => u.Departments)
                .Include(t => t.UsersTeam)
                 .ThenInclude(n => n.Teams)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
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


            var users = await _context.Users
            .Include(r => r.UsersRole)
             .ThenInclude(e => e.Role)
            .Include(d => d.UsersDepartment)
             .ThenInclude(u => u.Departments)
            .Include(t => t.UsersTeam)
             .ThenInclude(n => n.Teams)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ID == id);

            if (users == null)
            {
                return NotFound();
            }
            PopulateUsersRoles(users);
            PopulateUsersDepartment(users);
            PopulateUsersTeam(users);

            return View(users);
        }

        private void PopulateUsersRoles(Users users)
        {
            var allRoles = _context.Roles;
            var userRoles = new HashSet<int>(users.UsersRole.Select(c => c.Role.ID));
            var viewModel = new List<UserRoles>();
            foreach (var role in allRoles)
            {
                viewModel.Add(new UserRoles
                {
                    RolesID = role.ID,
                    UsersID = users.ID,
                    Role = role,
                    User = users                   
                });
            }
            ViewData["Roles"] = viewModel;
        }

        private void PopulateUsersDepartment(Users users)
        {
            var allDepartment = _context.Department;
            var userDepartments = new HashSet<int>(users.UsersDepartment.Select(c => c.Departments.ID));
            var viewModel = new List<UserDepartment>();
            foreach (var department in allDepartment)
            {
                viewModel.Add(new UserDepartment
                {
                    DepartmentID = department.ID,
                    UsersID = users.ID,
                    Departments = department,
                    User = users
                });
            }
            ViewData["Department"] = viewModel;
        }

        private void PopulateUsersTeam(Users users)
        {
            var allTeam = _context.Team;
            var userTeams = new HashSet<int>(users.UsersTeam.Select(c => c.Teams.ID));
            var viewModel = new List<UserTeam>();
            foreach (var team in allTeam)
            {
                viewModel.Add(new UserTeam
                {
                    TeamID = team.ID,
                    UsersID = users.ID,
                    Teams = team,
                    User = users
                });
            }
            ViewData["Team"] = viewModel;
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedRoles, string[] selectedDepartment, string[] selectedTeam)

        {
            if (id == null)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users
                .Include(r => r.UsersRole)
                 .ThenInclude(e => e.Role)
                .Include(d => d.UsersDepartment)
                 .ThenInclude(u => u.Departments)
                .Include(t => t.UsersTeam)
                 .ThenInclude(n => n.Teams)
                .SingleOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Users>(
                userToUpdate,
                "",
                s => s.FirstName, s => s.LastName, s => s.UsersRole, s => s.UsersDepartment, s => s.UsersTeam))
            {

                UpdateUserRoles(selectedRoles, userToUpdate);
                UpdateUserDepartment(selectedDepartment, userToUpdate);
                UpdateUserTeam(selectedTeam, userToUpdate);

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


        //Edit Roles
        private void UpdateUserRoles(string[] selectedRoles, Users userToUpdate)
        {
            if (selectedRoles == null)
            {
                userToUpdate.UsersRole = new List<UserRoles>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var userRoles = new HashSet<int>
                (userToUpdate.UsersRole.Select(c => c.Role.ID));
            foreach (var role in _context.Roles)
            {
                if (selectedRolesHS.Contains(role.ID.ToString()))
                {
                    if (!userRoles.Contains(role.ID))
                    {
                        userToUpdate.UsersRole.Add(new UserRoles { UsersID = userToUpdate.ID, RolesID = role.ID });
                    }
                }
                else
                {

                    if (userRoles.Contains(role.ID))
                    {
                        UserRoles roleToRemove = userToUpdate.UsersRole.SingleOrDefault(i => i.RolesID == role.ID);
                        _context.Remove(roleToRemove);
                    }
                }
            }
        }


        //Edit Department
        private void UpdateUserDepartment(string[] selectedDepartment, Users userToUpdate)
        {
            if (selectedDepartment == null)
            {
                userToUpdate.UsersDepartment = new List<UserDepartment>();
                return;
            }

            var selectedDepartmentHS = new HashSet<string>(selectedDepartment);
            var userDepartment = new HashSet<int>
                (userToUpdate.UsersDepartment.Select(c => c.Departments.ID));
            foreach (var department in _context.Department)
            {
                if (selectedDepartmentHS.Contains(department.ID.ToString()))
                {
                    if (!userDepartment.Contains(department.ID))
                    {
                        userToUpdate.UsersDepartment.Add(new UserDepartment { UsersID = userToUpdate.ID, DepartmentID = department.ID });
                    }
                }
                else
                {

                    if (userDepartment.Contains(department.ID))
                    {
                        UserDepartment departmentToRemove = userToUpdate.UsersDepartment.SingleOrDefault(i => i.DepartmentID == department.ID);
                        _context.Remove(departmentToRemove);
                    }
                }
            }
        }


        //Edit Team
        private void UpdateUserTeam(string[] selectedTeam, Users userToUpdate)
        {
            if (selectedTeam == null)
            {
                userToUpdate.UsersTeam = new List<UserTeam>();
                return;
            }

            var selectedTeamHS = new HashSet<string>(selectedTeam);
            var userTeam = new HashSet<int>
                (userToUpdate.UsersTeam.Select(c => c.Teams.ID));
            foreach (var team in _context.Team)
            {
                if (selectedTeamHS.Contains(team.ID.ToString()))
                {
                    if (!userTeam.Contains(team.ID))
                    {
                        userToUpdate.UsersTeam.Add(new UserTeam { UsersID = userToUpdate.ID, TeamID = team.ID });
                    }
                }
                else
                {

                    if (userTeam.Contains(team.ID))
                    {
                        UserTeam teamToRemove = userToUpdate.UsersTeam.SingleOrDefault(i => i.TeamID == team.ID);
                        _context.Remove(teamToRemove);
                    }
                }
            }
        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users

                .Include(r => r.UsersRole)
                 .ThenInclude(e => e.Role)
                .Include(d => d.UsersDepartment)
                 .ThenInclude(u => u.Departments)
                .Include(t => t.UsersTeam)
                 .ThenInclude(n => n.Teams)
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
                .Include(r => r.UsersRole)
                 .ThenInclude(e => e.Role)
                .Include(d => d.UsersDepartment)
                 .ThenInclude(u => u.Departments)
                .Include(t => t.UsersTeam)
                 .ThenInclude(n => n.Teams)
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
