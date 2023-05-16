using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        // Introduce the model to the database that eventually become an entity
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Profilings> Profilings { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Universities> Universities { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<AccountRoles> AccountRoles { get; set; }
        public DbSet<Accounts> Accounts { get; set; }

        //Fluent API --Relation Configuration--
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*Relasi antar tabel cukup ditulis 1 tabel saja*/

            //One University has many Educations
            modelBuilder.Entity<Universities>()
                .HasMany(u => u.Educations)
                .WithOne(e => e.Universities)
                .HasForeignKey(e => e.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);

            //One Education has one Profiling
            modelBuilder.Entity<Educations>()
                .HasOne(e => e.Profilings)
                .WithOne(p => p.Educations)
                .HasForeignKey<Profilings>(p => p.EducationId)
                .OnDelete(DeleteBehavior.Cascade);

            //One Profiling has One Employee
            modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Profilings)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Profilings>(p => p.EmployeeNIK)
                    .OnDelete(DeleteBehavior.Restrict);

            //One Account has One Employee
            modelBuilder.Entity<Employee>()
                .HasOne(em => em.Accounts)
                .WithOne(ac => ac.Employees)
                .HasForeignKey<Accounts>(ac => ac.EmployeeNIK)
                .OnDelete(DeleteBehavior.Restrict);

            //One Account has many Account Roles
            modelBuilder.Entity<Accounts>()
                .HasMany(ac => ac.AccountRoles)
                .WithOne(a => a.Accounts)
                .HasForeignKey(a => a.AccountNIK)
                .OnDelete(DeleteBehavior.Restrict);

            //One Role has many Account Roles
            modelBuilder.Entity<Roles>()
                .HasMany(r => r.AccountRoles)
                .WithOne(a => a.Roles)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
