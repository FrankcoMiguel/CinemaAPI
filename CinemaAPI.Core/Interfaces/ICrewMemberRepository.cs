using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface ICrewMemberRepository
    {
        Task<IEnumerable<CrewMember>> GetCrewMembers();
        Task<CrewMember> GetCrewMember(int id);
    }
}
