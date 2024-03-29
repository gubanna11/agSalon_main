using agSalon.Data;
using agSalon.Data.Services;
using agSalon.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddDbContext<AppDbContext>(options => options.UseMySql(
            //    "server=localhost;user=root;password=root;database=test_ag;",
            //    new MySqlServerVersion(new Version(8, 0, 11))
            //));

            services.AddDbContext<AppDbContext>(options => options.UseMySql(
                 Configuration.GetConnectionString("DefaultConnectionString"),
                 new MySqlServerVersion(new Version(8, 0, 11))));


            services.AddScoped<IAttendancesService, AttendancesService>();
            services.AddScoped<IGroupsService, GroupsService>();
            services.AddScoped<IServicesService, ServicesService>();


            services.AddIdentity<Client, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

			AppDbInitializer.Seed(app);
			AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
            
        }
    }
}
