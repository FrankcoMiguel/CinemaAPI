using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class CastingRepository : BaseRepository<Casting>, ICastingRepository
    {
        public CastingRepository(CinemaAPIContext context) : base(context) { }
    }
}