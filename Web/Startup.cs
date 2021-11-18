using Data.Abstract;
using Data.Concrete;
using Data.Concrete.Ef;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Identity;

namespace Web
{
    public class Startup
    {
        private readonly string name="AddCors";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name:name,builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddControllersWithViews();

            services.AddScoped<IXercTeyinatiDal,EfXercTeyinatiDal>();
            services.AddScoped<IXercDal,EfXercDal>();
            
            services.AddDbContext<OfficeContext>();
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<OfficeContext>();
            services.Configure<IdentityOptions>(options=> { });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.Cookie = new CookieBuilder()
                {
                    HttpOnly = true,
                    Name="_OfficeManagement",
                    SameSite=SameSiteMode.Strict
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
            //    RequestPath="/modules"
            //});
            app.UseCors(name);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=home}/{action=index}/{id?}"
                    );
            });
            IdentitySeed.Seed(userManager, roleManager, configuration).Wait();
        }
    }
}
