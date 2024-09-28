  using CompanyMvc.Dox.BLL.Interfaces;
using CompanyMvc.Dox.BLL.Repositories;
using CompanyMvc.Dox.BLL.Units;
using CompanyMvc.Dox.DAL.Data.Contexts;
using CompanyMvc.Dox.DAL.Model;
using CompanyMvc.Dox.PL.Mapping.Employees;
using CompanyMvc.Dox.PL.Services;
using Microsoft.AspNetCore.Identity;
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
           builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();//add dependency Injection


            builder.Services.AddScoped<IScopedService,ScopedService>();         
            builder.Services.AddTransient<ITransientService,TransientService>();
            builder.Services.AddSingleton<ISingleTonService,SingleTonService>();


            builder.Services
                .AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
			builder.Services.AddAutoMapper(typeof(EmployeeProfile));

            builder.Services.ConfigureApplicationCookie(
                config =>
                {
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                }


            ); 


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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
