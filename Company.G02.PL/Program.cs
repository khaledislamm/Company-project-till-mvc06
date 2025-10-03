using Company.G02.BLL;
using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Reposatiories;
using Company.G02.DAL.Data.Contexts;
using Company.G02.PL.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Company.G02.PL
{
    public class Program
    {
        // 3-Tier (Layer) Architecture
        // 3.1. Data Access Layer (Models - DbContext - Configurations)
        // 3.2. Business Logic Layer (Repository Dp - Unit Of Work DP)
        // 3.3. Presentation Layer (MVC - Apis - Mobile - Desktop - Console)

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Register Built-in MVC Services
            //builder.Services.AddScoped<IDepratmentRepository,DepartmentRepository>(); // Allow DI For DepartmentRepositorys
            
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allow DI For EmployeeRepositorys

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Allow DI For UnitOfWork

            // builder.Services.AddScoped<CompanyDbContext>(); 
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Allow DI For CompanyDbContext

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            // Life Time
            //builder.Services.AddScoped();    // Create Object Life Time Per Request - Un Reachable Object
            //builder.Services.AddTransient(); // Create Object Life Time Per Operation 
            //builder.Services.AddSingleton(); // Create Object Life Time Per App

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
