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
            new Users{FirstName="Awe",LastName="Avatar"},
            new Users{FirstName="Awe2",LastName="Avatar"},
            new Users{FirstName="Awe3",LastName="Avatar"},
            new Users{FirstName="Awe4",LastName="Avatar"},
            new Users{FirstName="Awe5",LastName="Avatar"},
            new Users{FirstName="Awe6",LastName="Avatar"},
            new Users{FirstName="Awe7",LastName="Avatar"},
            new Users{FirstName="Awe8",LastName="Avatar"},
            new Users{FirstName="Awe9",LastName="Avatar"},
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
            new Roles{RoleName="ProjectManager"},
            new Roles{RoleName="Tester" },
            new Roles{RoleName="Admin" },
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




            }
            /*
            foreach (UserRoles r in userRoles)
            {
                context.UserRole.Add(r);
            }*/

            /*
            var team = new Team[]
            {
            new Team{ID=1,UsersTeams=Team.TeamEnum.ATeam},
            new Team{ID=1,UsersTeams=Team.TeamEnum.GoldTeam},
            new Team{ID=2,UsersTeams=Team.TeamEnum.Wombats},
            new Team{ID=3,UsersTeams=Team.TeamEnum.ATeam},
            new Team{ID=4,UsersTeams=Team.TeamEnum.GoldTeam},
            new Team{ID=5,UsersTeams=Team.TeamEnum.Wombats},
            new Team{ID=6,UsersTeams=Team.TeamEnum.ATeam},
            new Team{ID=7,UsersTeams=Team.TeamEnum.GoldTeam},
            new Team{ID=7,UsersTeams=Team.TeamEnum.Wombats},
            new Team{ID=7,UsersTeams=Team.TeamEnum.ATeam},
            new Team{ID=8,UsersTeams=Team.TeamEnum.ATeam},
            new Team{ID=9,UsersTeams=Team.TeamEnum.ATeam},
            new Team{ID=10,UsersTeams=Team.TeamEnum.Wombats},
            new Team{ID=10,UsersTeams=Team.TeamEnum.GoldTeam},
            };
            foreach (Team t in team)
            {
                context.UsersTeam.Add(t);
            }
            context.SaveChanges();
            */
            var department = new Department[]
           {
                new Department{UserID=1,UserDepartment=Department.Departments.Consulting},
                new Department{UserID=2,UserDepartment=Department.Departments.ProductDevelopment },
                new Department{UserID=3,UserDepartment=Department.Departments.Marketing },
                new Department{UserID=4,UserDepartment=Department.Departments.Marketing },
                new Department{UserID=5,UserDepartment=Department.Departments.ProductDevelopment },
                new Department{UserID=6,UserDepartment=Department.Departments.Marketing },
                new Department{UserID=7,UserDepartment=Department.Departments.ProductDevelopment },
                new Department{UserID=8,UserDepartment=Department.Departments.Consulting},
                new Department{UserID=9,UserDepartment=Department.Departments.Consulting},
                new Department{UserID=10,UserDepartment=Department.Departments.Consulting},

           };
            foreach (Department d in department)
            {
                context.Department.Add(d);
            }
            context.SaveChanges();


        }
    }




}


