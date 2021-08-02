using System.Linq;
using CymaxAssessmentAPI.Models;
using CymaxAssessmentAPI.Repositories;
using CymaxAssessmentAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CymaxAssessmentAPI
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
            services.AddScoped<ICartonRepository, CartonRepository>();
            services.AddScoped<ICartonService, CartonService>();
            services.AddDbContext<CartonContext>(o => o.UseSqlite("Data source=cartons.db"));
            services.AddControllers(o =>
            {
                o.RespectBrowserAcceptHeader = true;
                o.InputFormatters.Add(new XmlSerializerInputFormatter(o));
                o.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            })
                    .AddXmlSerializerFormatters()
                    .AddXmlDataContractSerializerFormatters();

            services.AddMvcCore().AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();

            services.AddMvcCore(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter(config));
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CymaxAssessmentAPI", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CymaxAssessmentAPI v1"));
            }

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
