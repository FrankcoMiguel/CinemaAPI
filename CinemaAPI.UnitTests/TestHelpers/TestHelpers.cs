using AutoMapper;
using CinemaAPI.Infrastructure.Data;
using CinemaAPI.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace CinemaAPI.UnitTests.TestHelpers
{
    public static class TestHelpers
    {
        public static CinemaAPIContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CinemaAPIContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new CinemaAPIContext(options);
        }

        public static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperProfile>();
            });

            return configuration.CreateMapper();
        }
    }
}