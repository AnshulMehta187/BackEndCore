using API.Extensions;
using API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models.Models;
using Serilog;
using System.IO;

namespace ReviseBackEndApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithThreadId()
                .CreateLogger();

            Log.Information("API started running");
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            #region Install SwashBuckle package and then register swagger generator from nuget.org
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            #endregion
            services
            .AddDbContext<TestContext>(options =>
               options.UseLazyLoadingProxies()
               .UseSqlServer(connectionString: Configuration.GetConnectionString("Test"))
               );
            services.AddMvc();
            services.ConfigureDependencyInjection();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddSerilog();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(builder => builder
                                    .WithOrigins(Configuration.GetSection("AllowedOrigin:URL").Get<string[]>())
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()

            ); ;

            app.UseRequestResponseLoggingMiddleware();

            app.UseResponseWrapperMiddleWare();

            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
