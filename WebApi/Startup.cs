using Business.Abstract;
using Business.Concrete;
using Core.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
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
            services.AddHttpContextAccessor();
            

            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPizzaService, PizzaManager>();
            services.AddSingleton<IPizzaDal, EfPizzaDal>();
            services.AddSingleton<IAuthService, AuthManager>();
            services.AddSingleton<IOrderService, OrderManager>();
            services.AddSingleton<IOrderDal, EfOrderDal>();
            services.AddSingleton<IOrderHelperService, OrderHelperManager>();
            services.AddSingleton<IOrderHelperDal, EfOrderHelperDal>();
            services.AddSingleton<ICategoryService, CategoryManager>();
            services.AddSingleton<ICategoryDal, EfCategoryDal>();
            services.AddSingleton<ISizeDal, EfSizeDal>();
            services.AddSingleton<ISizeService, SizeManager>();
            services.AddSingleton<ILoginInfoDal, EfLoginInfoDal>();
            services.AddSingleton<ILoginInfoService, LoginInfoManager>();
            services.AddSingleton<IUserDal, EfUserDal>();
            services.AddSingleton<IUserService, UserManager>();
            services.AddSingleton<IStaffService, StaffManager>();
            services.AddSingleton<IStaffDal, EfStaffDal>();
            services.AddSingleton<IStatusService, StatusManager>();
            services.AddSingleton<IStatusDal, EfStatusDal>();
            services.AddSingleton<ICommentDal, EfCommentDal>();
            services.AddSingleton<ICommentService, CommentManager>();
            services.AddSingleton<ICampaignSliderService, CampaignSliderManager>();
            services.AddSingleton<ICampaignSliderDal, EfCampaignSliderDal>();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new ErrorResult(exception.Message));
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                await next();

                if (!Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
            app.UseStaticFiles();
            //var staticFilePath = Directory.GetParent(Environment.CurrentDirectory) + "\\PizzaImg\\";
            /*var staticFilePath = "\\PizzaImg\\";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                staticFilePath),
                RequestPath = "/PizzaImg"
            }); */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
