namespace FishMap.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using FishMap.Data;
    using FishMap.Data.Common;
    using FishMap.Data.Common.Repositories;
    using FishMap.Data.Models;
    using FishMap.Data.Repositories;
    using FishMap.Data.Seeding;
    using FishMap.Services;
    using FishMap.Services.Data;
    using FishMap.Services.Data.Contracts;
    using FishMap.Services.Mapping;
    using FishMap.Services.Messaging;
    using FishMap.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ITripsService, TripsService>();
            services.AddTransient<IFishSpeciesService, FishSpeciesService>();
            services.AddTransient<IFishServices, FishServices>();
            services.AddTransient<IImageServices, ImageServices>();
            services.AddTransient<IGroupTripsService, GroupTripsService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFishingMethodService, FishingMethodService>();

            Account account = new Account(
                this.configuration["Cloudinary:AppName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);
            services.AddTransient<ICloudinaryService, CloudinaryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
