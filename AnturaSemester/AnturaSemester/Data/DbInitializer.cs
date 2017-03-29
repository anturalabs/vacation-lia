using System;
using System.Collections.Generic;
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

            

            var roles = new Roles[]
            {
            new Roles{UserID=1,UserRole=Roles.Role.Programmer},
            new Roles{UserID=1,UserRole=Roles.Role.ProjectManager},
            new Roles{UserID=2,UserRole=Roles.Role.Tester },
            new Roles{UserID=3,UserRole=Roles.Role.Tester },
            new Roles{UserID=4,UserRole=Roles.Role.Admin },
            new Roles{UserID=5,UserRole=Roles.Role.Programmer },
            new Roles{UserID=6,UserRole=Roles.Role.Programmer },
            new Roles{UserID=7,UserRole=Roles.Role.Admin },
            new Roles{UserID=7,UserRole=Roles.Role.Tester },
            new Roles{UserID=7,UserRole=Roles.Role.Admin },
            new Roles{UserID=8,UserRole=Roles.Role.Programmer},
            new Roles{UserID=9,UserRole=Roles.Role.Programmer},
            new Roles{UserID=10,UserRole=Roles.Role.Programmer},
            new Roles{UserID=10,UserRole=Roles.Role.Tester },
            };
            foreach (Roles r in roles)
            {
                context.UserRole.Add(r);
            }
            context.SaveChanges();
        }
}



        
        }
  
