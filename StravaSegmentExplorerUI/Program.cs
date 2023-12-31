using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Identity.Client;
using StravaSegmentExplorerDataAccess.API;
using StravaSegmentExplorerDataAccess.Models;
using StravaSegmentExplorerDataAccess.SQLServer;
using StravaSegmentExplorerUI.Data;
using StravaSegmentExplorerUI.Models;

namespace StravaSegmentExplorerUI
{
    public class Program
    {
        public static void Main(string[] args)
    {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            

            var connectionString = builder.Configuration.GetConnectionString("IdentityDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<StravaAPIDataAccess>();
            builder.Services.AddScoped<StravaSegmentExplorerUI.Controllers.ConnectToStravaController>();

            builder.Services.AddMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".SegmentExplore.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}