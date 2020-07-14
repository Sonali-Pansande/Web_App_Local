using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Web_App_Local.CustomFilters;
using Web_App_Local.Data;
using Web_App_Local.Middlewares;
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

        
            // 1.
            services.AddDbContext<SecurityDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("SecurityDbContextConnection")
                )
           );

            //2.
            services.AddDefaultIdentity<IdentityUser>(
                options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SecurityDbContext>();

            //3.
            services.AddDbContext<LogDbContext>(options =>
       options.UseSqlServer(
           Configuration.GetConnectionString("AppDbConnection")
           ));

            //4.
            services.AddScoped<CorAuthService>();

            // 5.
            byte[] secretKey = Convert.FromBase64String(Configuration["JWTCoreSettings:SecretKey"]);

            //6.

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure the swagger service
            services.AddSwaggerGen(C => 
                {
                    C.SwaggerDoc("v1",
                        new OpenApiInfo { Title = "ASP.NET Core API", Version = "V1" });
                });

            // add the distributed memory cache service and session
            services.AddDistributedMemoryCache();
            services.AddSession( session =>
            {
                session.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddScoped<IRepository<Category, int>, CategoryRepository>();

            services.AddScoped<IRepository<Product, int>, ProductRepository>();

            //  services.AddScoped<CustomExceptionFilter>();
            //  services.AddScoped<IRepository<CustomException, int>, ExceptionRepository>();

            services.AddControllersWithViews(options =>
            {
                //    options.Filters.Add(typeof( CustomExceptionFilter));
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
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

            // configure the swagger middleware
            app.UseSwagger();

            // provide / respond the HTML UI
            app.UseSwaggerUI(C =>
            {
                C.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core API Documentation");
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();//the session

            app.UseAuthentication();

            app.UseAuthorization();

            app.CustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
