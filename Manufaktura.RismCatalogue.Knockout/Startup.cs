﻿using Manufaktura.RismCatalogue.Shared.Services;
using Manufaktura.RismCatalogue.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Manufaktura.RismCatalogue.Knockout
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
            services.AddMvc();
            services.AddDbContext<RismDbContext>(c => c.UseMySql("server=localhost;database=manufaktura-rism;uid=admin;pwd=123123"));
            services.AddSingleton<SettingsService>();
            services.AddSingleton<PlaineAndEasieService>();
            services.AddSingleton<ScoreRendererService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseBlazor<Blazor.Startup>();

            var db = serviceProvider.GetRequiredService<RismDbContext>();
            db.Database.EnsureCreated();
        }
    }
}