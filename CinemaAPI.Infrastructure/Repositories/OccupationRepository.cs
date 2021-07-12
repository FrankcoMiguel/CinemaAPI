using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class OccupationRepository : BaseRepository<Occupation>, IOccupationRepository
    {
        public OccupationRepository(CinemaAPIContext context) : base(context) { }
    }
}
