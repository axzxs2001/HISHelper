using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProductReleaseSystem;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Http.Features;
using ProductReleaseSystem.Data;
using ProductReleaseSystem.Models.IRepository;
using ProductReleaseSystem.Models.Repository;
using ProductReleaseSystem.Models.Data;
using UEditorNetCore;
using Microsoft.AspNetCore.Http;

namespace ProductReleaseSystem
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)      //Startup构造，加载配置文件
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }     //配置文件实体


        public void ConfigureServices(IServiceCollection services)     //注入服务到容器中
        {
            //验证码类
            services.AddTransient<VierificationCodeServices, VierificationCodeServices>();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(120000);
                options.CookieHttpOnly = true;
            });
            services.Configure<FormOptions>(x =>
            {

                x.ValueLengthLimit = int.MaxValue;

                x.MultipartBodyLengthLimit = int.MaxValue;

            });

            //用于生成EF的ProductRelease数据库连接字符串
            var prconntion = Configuration.GetConnectionString("prConnectionStrings");

            services.Configure<ConnectionSetting>(Configuration.GetSection("ConnectionStrings"));
            //上传文件
            services.AddTransient<IUploadFile, UploadFile>();
            //在研项目
            services.AddTransient<IResearch, Research>();
            services.AddTransient<IDemand, Demand>();

            services.AddUEditorService()
                .Add("test", context =>
                 {
                     context.Response.WriteAsync("from test action");
                 })
                .Add("test2", context =>
                 {
                     context.Response.WriteAsync("from test2 action");
                 }
                );
            services.AddMvc();



        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)   //添加中间件到Request请求管道中
        {

            // 验证权限 为权限添加中间件
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //验证方案名称
                AuthenticationScheme = "loginvalidate",
                //没有权限时导航的登录action
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/login"),
                //访问被拒绝后的acion
                AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/authority"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                SlidingExpiration = true
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // 添加NLog到.net core框架中
            loggerFactory.AddNLog();
            //添加NLog的中间件
            app.AddNLogWeb();
            // 指定NLog的配置文件
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
