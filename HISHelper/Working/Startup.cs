using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Working.Models.DataModel;
using Microsoft.EntityFrameworkCore;
using Working.Model.Repository;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace Working
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

  
        public void ConfigureServices(IServiceCollection services)
        {  
            //添加数据操作
            var connection = string.Format(Configuration.GetConnectionString("DefaultConnection"), System.IO.Directory.GetCurrentDirectory());         
            //添加数据实体
            services.AddDbContext<WorkingDbContext>(options => options.UseSqlite(connection));


            services.AddTransient<IUserResitory, UserResitory>();
            services.AddTransient<IWorkItemResitory, WorkItemResitory>();
            services.AddMvc();
        }

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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkingDbContext>
    //{
    //    public WorkingDbContext CreateDbContext(string[] args)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json")
    //            .Build();
    //        var builder = new DbContextOptionsBuilder<WorkingDbContext>();
    //        var connectionString = configuration.GetConnectionString("DefaultConnection");
    //        builder.UseSqlite(connectionString);
    //        return new WorkingDbContext(builder.Options);
    //    }
    //}
}
