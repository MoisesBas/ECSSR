using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECSSR.COMMON;
using ECSSR.DOMAIN;
using ECSSR.UTILITY.ElasticSearch;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Others;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ECSSR.API
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
            services.AddControllers();
            services.AddOptions();
            services.AddDomainAutoMapper();
            services.AddApplication();
            services.AddDomainCommon();
            services.AddSingleton(Configuration);
            services.AddTransient<IRepositoryBase, ProductRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.Configure<ECSSRSettings>(option => Configuration.GetSection("elasticsearch").Bind(option));
            services.AddDbContext<ECSSRDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IECSSRDbContext>(provider => provider.GetService<ECSSRDbContext>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECSSR Exam API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ECSSRDbContext dataContext)
        {
            dataContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECSSR Exam API");
            });
        }
    }
}
