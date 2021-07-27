using Autofac;
using Autofac.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZFStudio.Data;

namespace ZFStudio.Web
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
            #region 注册Session
            services.AddSession();
            #endregion


            services.AddTransient<IExceptionFilter, Filters.CustomerExceptionFilter>();
            //services.AddControllersWithViews();
            services.AddControllersWithViews(options=> {
                options.Filters.Add<Filters.CustomerExceptionFilter>();//全局注册
            });

            services.AddDbContext<MyDbContext>(options =>
            {
                var connStr = Configuration.GetConnectionString("MyConn");
                options.UseSqlServer(connStr);
            });

            //services.AddTransient<IUserService, UserService>();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();//授权
            //app.UseAuthentication 身份认证

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// AutoFac注册
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //1.直接在此方法注册
            //builder.RegisterType<UserService>().As<IUserService>();

            //2.通过定义类（如CustomAutofacModule）并实现Module，在此类重写Load方法，在Load方法中同1中注册
            //builder.RegisterModule(new CustomAutofacModule());

            //3.通过配置文件反射注册
            var configBuiler = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource()
            {
                Path = @"ConfigFiles\autofac.json",
                Optional = false,
                ReloadOnChange = true
            }).Build();
            var module = new ConfigurationModule(configBuiler); 
            builder.RegisterModule(module);
        }
    }
}
