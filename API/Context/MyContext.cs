using System;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.Id_user);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.Id_role);

            modelBuilder.Entity<Jobs>()
                .HasOne(x => x.Companys)
                .WithMany(sx => sx.Jobess)
                .HasForeignKey(dd => dd.Id_company);
        }

        public DbSet<Acceptment> Acceptments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Class> Classes { get; set; }

    }
}
