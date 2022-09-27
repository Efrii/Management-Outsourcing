using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Middleware;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CLIENT
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
            services.AddControllersWithViews();

            //Use Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // JWT ser
            services.AddTokenAuthentication(Configuration);

            // Depedency Repository
            // Tambahkan Depedency Repository setiap membuat repository baru
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<AccountRepository>();
            services.AddScoped<CompanyRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<InterviewRepository>();
            services.AddScoped<JobsRepository>();
            services.AddScoped<ScoreRepository>();
            services.AddScoped<ClassRepository>();
            services.AddScoped<CVRepository>();
            services.AddScoped<AcceptmentRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // use status code
            app.UseStatusCodePages(async context =>
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
                {

                    response.Redirect("/401");

                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
                {

                    response.Redirect("/404");

                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
                {

                    response.Redirect("/403");

                }
            });

            app.UseRouting();

            //Use Session
            app.UseSession();

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            //auth
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
