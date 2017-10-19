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
using Microsoft.AspNetCore.Http;

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
            services.AddTransient<IDepartmentResitory, DepartmentResitory>();

            //注入验证 2.0
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "loginvalidate";
                options.DefaultSignInScheme = "loginvalidate";
                options.DefaultAuthenticateScheme = "loginvalidate";
            })
            .AddCookie("loginvalidate", m =>
            {
                m.LoginPath = new PathString("/login");
                m.AccessDeniedPath = new PathString("/home/error");
                m.LogoutPath = new PathString("/logout");
                m.Cookie.Path = "/";
            });

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
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

 
}
