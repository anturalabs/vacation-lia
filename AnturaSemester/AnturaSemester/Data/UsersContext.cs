using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnturaSemester.Models;

namespace AnturaSemester.Data
{
    public class UsersContext: DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRole { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<UserDepartment> UserDepartment { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }
        public DbSet<CalendarCell> Calendar { get; set; }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<CalendarCell>().ToTable("Calendar");
            

            modelBuilder.Entity<UserRoles>()
              .HasKey(c => new { c.UsersID, c.RolesID });
            modelBuilder.Entity<UserDepartment>()
              .HasKey(c => new { c.UsersID, c.DepartmentID });
            modelBuilder.Entity<UserTeam>()
              .HasKey(c => new { c.UsersID, c.TeamID });

        }

     /*   protected override DbEntityValidationResult ValidateEntity(
                                        DbEntityEntry entityEntry,
                                        IDictionary<object, object> items)
        {
            //base validation for Data Annotations, IValidatableObject
            var result = base.ValidateEntity(entityEntry, items);

            //You can choose to bail out before custom validation
            //if (result.IsValid)
            //    return result;

            CustomValidate(result);
            return result;
        }

        private void CustomValidate(DbEntityValidationResult result)
        {
            ValidateContacts(result);
            ValidateOrganisation(result);
        }

        private void ValidateContacts(DbEntityValidationResult result)
        {
            var c = result.Entry.Entity as Contact;
            if (c == null)
                return;

            if (Contacts.Any(a => a.FirstName == c.FirstName
                                  && a.LastName == c.LastName
                                  && a.ID != c.ID))
                result.ValidationErrors.Add(
                                  new DbValidationError("Name",
                                        "Name already exists"));
        }

        private void ValidateOrganisation(DbEntityValidationResult result)
        {
            var organisation = result.Entry.Entity as Organisation;
            if (organisation == null)
                return;

            if (Organisations.Any(o => o.Name == organisation.Name
                                       && o.ID != organisation.ID))
                result.ValidationErrors.Add(
                                      new DbValidationError("Name",
                                            "Name already exists"));
        } */



    }
}
