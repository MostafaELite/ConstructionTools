using ConstructionTools.DataAccess;
using ConstructionTools.Repository.Abstract;
using ConstructionTools.Repository.Concreate;
using ConstructionTools.Services.Abstract;
using ConstructionTools.Services.Concreate;
using ConstructionTools.Services.Concreate.FeesCalculators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConstructionTools.Api
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
            services.AddCors(o => o.AddPolicy("Public", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddDbContext<ConstructionToolsDb>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<DbContext, ConstructionToolsDb>();
            services.AddTransient<FeesCalculatorFactory>();
            services.AddTransient<SpecializedToolsFeesCalculator>();
            services.AddTransient<HeavyToolsFeesCalculator>();
            services.AddTransient<RegularToolsFeesCalculator>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IConstructionToolsService, ConstructionToolsService>();
            services.AddTransient(typeof(IRepository<>), typeof(SqlRepository<>));
            services.AddMemoryCache();
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
              
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();


        }
    }
}
