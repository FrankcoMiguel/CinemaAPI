using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Repositories;
using CinemaAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using CinemaAPI.Infrastructure.Filters;
using FluentValidation.AspNetCore;
using CinemaAPI.Core.Services;

namespace CinemaAPI.Api
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options => {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options => 
            {
                //options.SuppressModelStateInvalidFilter = true;
            });


            services.AddDbContext<CinemaAPIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Dev")));

            //Services
            services.AddTransient<IAgeRatingService, AgeRatingService>();
            services.AddTransient<IGenreService, GenreService>();



            //Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();


            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options => 
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
        }
    }
}
