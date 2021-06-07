using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class CrewRoleRepository : ICrewRoleRepository
    {

        private readonly CinemaAPIContext _context;

        public CrewRoleRepository(CinemaAPIContext context)
        {
            _context = context;
        }

        public async Task<CrewRole> GetCrewRole(int id)
        {
            var crewRole = await _context.CrewRole.SingleAsync(x => x.CrewRoleId == id);
            return crewRole;
        }

        public async Task<IEnumerable<CrewRole>> GetCrewRoles()
        {
            var crewRoles = await _context.CrewRole.ToListAsync();
            return crewRoles;
        }
    }
}
