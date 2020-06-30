using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web_App_Local.CustomFilters;
using Web_App_Local.Data;
using Web_App_Local.Models;
using Web_App_Local.Services;

namespace Web_App_Local
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

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")
                )
           );


            services.AddDbContext<AppJune2020DbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("AppDbConnection")
                )
           );

            services.AddDbContext<LogDbContext>(options =>
           options.UseSqlServer(
               Configuration.GetConnectionString("AppDbConnection")
               )
          );



            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IRepository<Category, int>, CategoryRepository>();

            services.AddScoped<IRepository<Product, int>, ProductRepository>();

            services.AddScoped<CustomExceptionFilter>();
          //  services.AddScoped<IRepository<CustomException, int>, ExceptionRepository>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof( CustomExceptionFilter));
            });
            services.AddRazorPages();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
