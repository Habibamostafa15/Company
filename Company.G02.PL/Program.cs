using Company.BLL;
using Company.BLL.Interfaces;
using Company.BLL.Reposotiry;
using Company.DAL.Data.Context;
using Company.DAL.Entites;
using Company.DAL.Models;
using Company.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();  // register built in serices in MVC
           
            builder.Services.AddScoped<IDepartmentRepository,DepartmentReposirtory>();  //Allow DI in departmentRepoository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();  //Allow DI in EmployeeRepository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<DBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
                
                
                );
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));



            builder.Services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<DBContext>();

            builder.Services.ConfigureApplicationCookie(config =>
            {

                config.LoginPath = "/Account/SignIn";
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

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
