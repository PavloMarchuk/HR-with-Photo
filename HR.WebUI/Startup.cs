using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HR.DateLayer.DbLayer;
using Microsoft.EntityFrameworkCore;
using HR.WebUI.Models;
using HR.DateLayer.Repositories;

namespace HR.WebUI
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
            //додаємо посилання на провайдера сервера баз даних і додаємо строку підключення
            services.AddDbContext<HumanResourcesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //використовуємо встроєний контейнер створення залежностей
            //services.AddScoped<EmployeeModel, EmployeeModel>();

            services.AddScoped<DbContext, HumanResourcesContext>();
            services.AddTransient<IGenericRepository<Employee>, EmployeeRepository>();
            services.AddTransient<IGenericRepository<Photo>, PhotoRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
                    template: "{controller=Photo}/{action=List}/{id?}");
            });
        }
    }
}
