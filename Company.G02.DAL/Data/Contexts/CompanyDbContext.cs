using Company.G02.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Company.G02.DAL.Data.Contexts
{
    // CLR
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> optionss) : base(optionss)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = CompanyG02; Trusted_Connection = True; TrustServerCertificate = True");
        //}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
