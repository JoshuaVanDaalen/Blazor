using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using greenSacrifice.Blazor.Repo.Context;
using greenSacrifice.Blazor.Repo.Repos;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace greenSacrifice.Blazor
{
    public class AzureADOptionsContainer
    {
        public AzureADOptions _azureADStandard;
        public AzureADOptions _azureADPrivileged;

        public AzureADOptionsContainer(AzureADOptions azureADStandard, AzureADOptions azureADPrivileged)
        {
            _azureADStandard = azureADStandard;
            _azureADPrivileged = azureADPrivileged;
        }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add databases
            // TODO: Get the server and database values from Environment
            var conn = Configuration.GetConnectionString("HelloWorldDbContext");
            // if (Environment.EnvironmentName.EqualsIgnoreCase("LOCAL"){

            //conn = conn.Replace("{{SQLSERVER}}", @"(localdb)\MSSQLLocalDB");
            //conn = conn.Replace("{{SQLDATABASE}}", "greenSacrifice-Blazor-Local");
            //}
            //else { 
            conn = conn.Replace("{{SQLSERVER}}", @"sql-greensacrifice-blazor-dev.database.windows.net");
            conn = conn.Replace("{{SQLDATABASE}}", "sqldb-greensacrifice-blazor-dev");
            services.AddDbContextPool<HelloWorldDbContext>(options =>
                options.UseSqlServer(conn));

            // azure ad options
            var azureADStandard = new AzureADOptions();
            var azureADPrivileged = new AzureADOptions();

            azureADStandard = (AzureADOptions)Configuration.GetSection("AzureADStandard");
            azureADPrivileged = (AzureADOptions)Configuration.GetSection("AzureADPrivileged");
            Configuration.Bind("AzureADStandard", azureADStandard);
            Configuration.Bind("AzureADPrivileged", azureADPrivileged);


            AzureADOptionsContainer azureAdOptions = new(azureADStandard, azureADPrivileged);

            if (azureADStandard == null || azureADPrivileged == null)
            {
                throw new Exception("Missing Configuration for azureADStandard or azureADPrivileged");
            }

            services.AddSingleton(azureAdOptions);


            services.AddScoped<HelloWorldDbContext>();
            services.AddScoped<HelloWorldRepo>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
