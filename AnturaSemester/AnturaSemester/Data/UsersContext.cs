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
        public DbSet<Team> UsersTeam { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Team>().ToTable("Team");

            modelBuilder.Entity<UserRoles>()
              .HasKey(c => new { c.UsersID, c.RolesID });
        }
    }
}
