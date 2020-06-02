using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using dropShippingApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dropShippingApp.Models;
using dropShippingApp.Data.Repositories;
using dropShippingApp.Data.Repositories.RealRepos;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace dropShippingApp
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
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages();

            // force app to use login if jerk user attempts to use restricted controller
            services.ConfigureApplicationCookie(option => option.LoginPath = "/Login/Index");

            // injecting repos into controllers
            services.AddTransient<ICartRepo, RealCartRepo>();
            services.AddTransient<IActivityLogRepo, RealActivityLogRepo>();
            services.AddTransient<ICustomProductRepo, RealCustomProductRepo>();
            services.AddTransient<ILocationRepo, RealLocationRepo>();
            services.AddTransient<IOrderRepo, RealOrderRepo>();
            services.AddTransient<IPricingRepo, RealPricingRepo>();
            services.AddTransient<IProductColorRepo, RealColorRepo>();
            services.AddTransient<IQuestionMsgRepo, RealQuestionMessageRepo>();
            services.AddTransient<IQuestionResponseRepo, RealQuestionResponseRepo>();
            services.AddTransient<IRosterProductRepo, RealRosterProductRepo>();
            services.AddTransient<ISizeRepo, RealSizeRepo>();
            services.AddTransient<ITagRepo, RealTagRepo>();
            services.AddTransient<ITeamRepo, RealTeamRepo>();
            services.AddTransient<ITeamCreationReqRepo, RealTeamRequestRepo>();
            services.AddTransient<IUserRepo, RealUserRepo>();
            services.AddTransient<IProductSortRepo, RealProductSortRepo>();
            services.AddTransient<ITeamSortRepo, RealTeamSortRepo>();
            services.AddTransient<ITeamCategoryRepo, RealTeamCategoryRepo>();
            services.AddTransient<IProductCategoryRepo, RealProductCategoryRepo>();
            services.AddTransient<IProductGroupRepo, RealProductGroupRepo>();
            services.AddTransient<IRosterGroupRepo, RealRosterGroupRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // Ensure that the database has been created and the latest migration applied
            context.Database.Migrate();

            // seed DB
            SeedData.Seed(app.ApplicationServices);
        }
    }
}
