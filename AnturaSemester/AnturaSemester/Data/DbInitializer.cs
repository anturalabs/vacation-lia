using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AnturaSemester.Models;


namespace AnturaSemester.Data
{
    public class DbInitializer
    {
        public static void Initialize(UsersContext context)
        {
            context.Database.EnsureCreated();

            // Look for any roles.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            var users = new Users[]{};

            foreach (Users s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

            var calendars = new CalendarCell[]
          {
             //Removed test data because of GUID ID change
          };
            foreach (CalendarCell c in calendars)
            {
                context.Calendar.Add(c);
            }
            context.SaveChanges();


            var roles = new Roles[]
            {
            new Roles{RoleID="Antura Developers", RoleName="Developer" },
            new Roles{RoleID="Antura Development Architects", RoleName="Development Architect"},
            new Roles{RoleID="Antura Testers",RoleName="Tester" },
            new Roles{RoleID="Antura Support Engineers",RoleName="Support Engineer" },
            };
            foreach (Roles r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();

            var userRoles = new UserRoles[]
         {

         };
            foreach (UserRoles ur in userRoles)
            {
                var userRolesInDataBase = context.UserRole.Where(
                    s =>
                            s.User.ID == ur.UsersID &&
                            s.Role.ID == ur.RolesID).SingleOrDefault();
                if (userRolesInDataBase == null)
                {
                    context.UserRole.Add(ur);
                }
            }

            context.SaveChanges();

            var department = new Department[]
           {
                new Department{DepartmentName="ProductDevelopment"},
                new Department{DepartmentName="SalesDepartment"},
                new Department{DepartmentName="TestDepartment"},

           };
            foreach (Department d in department)
            {
                context.Department.Add(d);
            }
            context.SaveChanges();


            var userDepartment = new UserDepartment[]
            {

            };
            foreach (UserDepartment ud in userDepartment)
            {
                var userDepartmentInDataBase = context.UserDepartment.Where(
                    s =>
                            s.User.ID == ud.UsersID &&
                            s.Departments.ID == ud.DepartmentID).SingleOrDefault();
                if (userDepartmentInDataBase == null)
                {
                    context.UserDepartment.Add(ud);
                }
            }
            context.SaveChanges();


            var teams = new Team[]
          {
            new Team{TeamName="A-Team"},
            new Team{TeamName="GoldTeam"},
            new Team{TeamName="Wombats"},
          };
            foreach (Team t in teams)
            {
                context.Team.Add(t);
            }
            context.SaveChanges();

            var userTeam = new UserTeam[]
         {

         };
            foreach (UserTeam ut in userTeam)
            {
                var userTeamInDataBase = context.UserTeam.Where(
                    s =>
                            s.User.ID == ut.UsersID &&
                            s.Teams.ID == ut.TeamID).SingleOrDefault();
                if (userTeamInDataBase == null)
                {
                    context.UserTeam.Add(ut);
                }
            }
            context.SaveChanges();
        }
    }
}






