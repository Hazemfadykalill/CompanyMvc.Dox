using CompanyMvc.Dox.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMvc.Dox.DAL.Data.Contexts
{
    public class AppDbContext:DbContext
    {

        //To Connect Database
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(" Server = .;Database=DOX_MVC_S03;Trusted_Connection=True;TrustServerCertificate=True");
        //}

        //Configuration Class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //Or [To All Class In The Same Time]
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
        //Names Table That In Database
      
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
