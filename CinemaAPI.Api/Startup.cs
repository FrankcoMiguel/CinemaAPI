using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Repositories;
using CinemaAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

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
            services.AddControllers();

            services.AddDbContext<CinemaAPIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Dev")));

            services.AddTransient<IActorRepository, ActorRepository>();
            services.AddTransient<IDirectorRepository, DirectorRepository>();
            services.AddTransient<IFilmRepository, FilmRepository>();
            services.AddTransient<ICrewRepository, CrewRepository>();
            services.AddTransient<ICrewRoleRepository, CrewRoleRepository>();
            services.AddTransient<ICrewMemberRepository, CrewMemberRepository>();


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
