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

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new Users[]
            {
            new Users{FirstName="Awe",LastName="Godis"},
            new Users{FirstName="Test",LastName="Testson"},
            new Users{FirstName="Sofie",LastName="Choklad"},
            new Users{FirstName="Robin",LastName="Kandetmesta"},
            new Users{FirstName="Amadeus",LastName="Mozart"},
            new Users{FirstName="Varför",LastName="Inte"},
            new Users{FirstName="Awe",LastName="Risk"},
            new Users{FirstName="Hitler",LastName="Paddan"},
            new Users{FirstName="Klar",LastName="Av"},
 };
            foreach (Users s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

            var calendars = new CalendarCell[]
          {
             new CalendarCell {UsersID=1, Date=DateTime.ParseExact("20170412", "yyyyMMdd", CultureInfo.InvariantCulture),AbsenceName="Holiday"},
             new CalendarCell {UsersID=2, Date=DateTime.ParseExact("20170413", "yyyyMMdd", CultureInfo.InvariantCulture),AbsenceName="VAB"},
             new CalendarCell {UsersID=1, Date=DateTime.ParseExact("20170414", "yyyyMMdd", CultureInfo.InvariantCulture),AbsenceName="Sick"},
             new CalendarCell {UsersID=3, Date=DateTime.ParseExact("20170411", "yyyyMMdd", CultureInfo.InvariantCulture), AbsenceName="VAB"},
             new CalendarCell {UsersID=5, Date=DateTime.ParseExact("20170411", "yyyyMMdd", CultureInfo.InvariantCulture), AbsenceName="Sick"}
          };
            foreach (CalendarCell c in calendars)
            {
                context.Calendar.Add(c);
            }
            context.SaveChanges();


            var roles = new Roles[]
            {
            new Roles{RoleName="Developer" },
            new Roles{RoleName="ProductOwner"},
            new Roles{RoleName="Tester" },
            new Roles{RoleName="Architect" },
            };
            foreach (Roles r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();

            var userRoles = new UserRoles[]
         {
            new UserRoles{RolesID=1,UsersID=1},
            new UserRoles{RolesID=2,UsersID=2},
            new UserRoles{RolesID=4,UsersID=3},
            new UserRoles{RolesID=3,UsersID=3},
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
            new UserDepartment{UsersID=1, DepartmentID=2},
            new UserDepartment{UsersID=2, DepartmentID=1},
            new UserDepartment{UsersID=3, DepartmentID=3},
            new UserDepartment{UsersID=4, DepartmentID=1},
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
            new UserTeam{UsersID=1, TeamID=3},
            new UserTeam{UsersID=2, TeamID=2},
            new UserTeam{UsersID=3, TeamID=1},
            new UserTeam{UsersID=4, TeamID=1},
            new UserTeam{UsersID=5, TeamID=2},
            new UserTeam{UsersID=6, TeamID=3},
            new UserTeam{UsersID=7, TeamID=2},

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

                        context.SaveChanges();


                    }
                }




            }
        }
    }
}



