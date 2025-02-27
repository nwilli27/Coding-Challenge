﻿using ClarkCodingChallenge.BusinessLogic.Interfaces;
using ClarkCodingChallenge.DataAccess;
using ClarkCodingChallenge.DataAccess.Implementations;
using ClarkCodingChallenge.DataAccess.Interfaces;
using ClarkCodingChallenge.DataMapping;
using ClarkCodingChallenge.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClarkCodingChallenge
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<IRepository<ContactEntity>, InMemoryRepository<ContactEntity>>();

            services.AddScoped<IContactsService, ContactsService>();
            services.AddTransient<ContactDataMapper>();
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
                app.UseExceptionHandler("/Contacts/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseEndpoints(routes =>
            {
                routes.MapRazorPages();
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Contacts}/{action=Index}/{id?}");
            });
        }
    }
}
