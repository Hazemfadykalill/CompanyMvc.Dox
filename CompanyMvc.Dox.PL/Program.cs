using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.BLL.Repositories;
using CompanyMvc.Dox.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompanyMvc.Dox.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<AppDbContext>();//add dependency Injection
           // builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(" Server = .;Database=DOX_MVC_S03;Trusted_Connection=True;TrustServerCertificate=True"));//add dependency Injection
           //or
            builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConections")));//add dependency Injection
               //or                                                                                                                                        //or
           // builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConections"]));//add dependency Injection
           builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();//add dependency Injection
           builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();//add dependency Injection
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
