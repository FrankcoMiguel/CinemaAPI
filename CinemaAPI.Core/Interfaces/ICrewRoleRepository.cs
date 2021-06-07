using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface ICrewRoleRepository
    {
        Task<IEnumerable<CrewRole>> GetCrewRoles();
        Task<CrewRole> GetCrewRole(int id);
    }
}
