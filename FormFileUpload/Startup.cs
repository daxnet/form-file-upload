using Amazon;
using Amazon.S3;
using FormFileUpload.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormFileUpload
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
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 268435456;
            });

            var s3Endpoint = Configuration["s3:endpoint"];
            var s3AccessKey = Configuration["s3:accessKey"];
            var s3SecretKey = Configuration["s3:secretKey"];

            var s3Configuration = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                ServiceURL = s3Endpoint,
                ForcePathStyle = true
            };

            services.AddTransient<IAmazonS3>(sp => new AmazonS3Client(s3AccessKey, s3SecretKey, s3Configuration));

            var mongoConnectionString = Configuration["mongo:connectionString"];
            var mongoDatabase = Configuration["mongo:database"];
            services.AddTransient<IDataAccessObject>(sp => new MongoDataAccessObject(new MongoUrl(mongoConnectionString), mongoDatabase));
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
