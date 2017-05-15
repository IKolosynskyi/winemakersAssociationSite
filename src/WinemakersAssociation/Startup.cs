using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WinemakersAssociation.Areas.Backend.Models.HomeModels;
using WinemakersAssociation.Business.Repositories.Implementations;
using WinemakersAssociation.Business.Repositories.Interfaces;
using WinemakersAssociation.Data.Entities;
using WinemakersAssociation.Persistence.Common;
using WinemakersAssociation.Areas.Backend.Models;
using EditModel = WinemakersAssociation.Areas.Backend.Models.PageContentModels.EditModel;

namespace WinemakersAssociation
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatatabase(Configuration.GetConnectionString("DefaultConnection"));

            services.AddMvc();

            services.AddScoped<IPageRepository, PageRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    "backendPage", "Backend", "backend/", new
                    {
                        controller = "Home",
                        action = "Index"
                    });

                routes.MapAreaRoute("frontendPage", "Frontend", "{type?}", new
                {
                    controller = "Page",
                    action = "Index"

                });
                routes.MapAreaRoute("frontenddef", "Frontend", "{controller}/{action}/{id?}", new
                {
                    controller = "Page",
                    action = "Index"

                });

                routes.MapRoute(
                    name: "default",
                    template: "{area=Frontend}/{controller=Page}/{action=Index}/{id?}");
            });

            AddMappings();
        }

        private void AddMappings()
        {
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<PageContentEntity, EditModel>()
                .ForMember(model => model.Type, expression => expression.MapFrom(e => e.Page.Type));
                mapper.CreateMap<EditModel, PageContentEntity>();

            });
        }
    }
}