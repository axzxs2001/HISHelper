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
using Microsoft.AspNetCore.Authentication.Cookies;

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
            services.AddDbContextPool<WorkingDbContext>(options => options.UseSqlite(connection));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            //注入验证 2.0
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme =  CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultSignInScheme =  CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme =  CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, m =>
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
