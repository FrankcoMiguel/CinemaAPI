using CinemaAPI.Core.Interfaces;
using CinemaAPI.Core.Options;
using CinemaAPI.Core.Services;
using CinemaAPI.Infrastructure.Data;
using CinemaAPI.Infrastructure.Interfaces;
using CinemaAPI.Infrastructure.Options;
using CinemaAPI.Infrastructure.Repositories;
using CinemaAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<CinemaAPIContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("Dev"))
            );

            return services;

        }


        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));

            return services; 
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Services
            services.AddTransient<IFilmService, FilmService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IOccupationService, OccupationService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IUserService, UserService>();


            //Repository and Unit Of Work
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Password Hasher
            services.AddSingleton<IPasswordService, PasswordService>();

            //Uri
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });

            return services;
        }

    }
}
