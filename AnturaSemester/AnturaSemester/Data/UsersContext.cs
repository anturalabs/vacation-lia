using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnturaSemester.Models;
using System.Globalization;


namespace AnturaSemester.Data
{
    public class UsersContext : DbContext
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

            modelBuilder.Entity<Roles>()
               .HasAlternateKey(c => c.RoleID);

            modelBuilder.Entity<UserRoles>()
              .HasKey(c => new { c.UsersID, c.RolesID });
            modelBuilder.Entity<UserDepartment>()
              .HasKey(c => new { c.UsersID, c.DepartmentID });
            modelBuilder.Entity<UserTeam>()
              .HasKey(c => new { c.UsersID, c.TeamID });
            modelBuilder.Entity<CalendarCell>()
                .HasKey(c => new { c.Date, c.UsersID });

        }
        /*
        protected override DbEntityValidationResult ValidateEntity(
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
            ValidateCell(result);
            
        }

        private void ValidateCell(DbEntityValidationResult result)
        {
            var c = result.Entry.Entity as CalendarCell;
            if (c == null)
                return;

            if (Calendar.Any(a => a.UsersID == c.UsersID
                                  && a.Date == c.Date
                                  && a.ID != c.ID))
                result.ValidationErrors.Add(
                                  new DbValidationError("Absence for this user already exists on the specified date"));
        }*/
    }
}
