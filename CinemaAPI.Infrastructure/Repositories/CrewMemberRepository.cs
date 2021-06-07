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
    public class CrewMemberRepository : ICrewMemberRepository
    {

        private readonly CinemaAPIContext _context;

        public CrewMemberRepository(CinemaAPIContext context)
        {
            _context = context;
        }

        public async Task<CrewMember> GetCrewMember(int id)
        {
            var crewMember = await _context.CrewMember.SingleAsync(x => x.CrewMemberId == id);
            return crewMember;
        }

        public async Task<IEnumerable<CrewMember>> GetCrewMembers()
        {
            var crewMembers = await _context.CrewMember.ToListAsync();
            return crewMembers;
        }
    }
}
