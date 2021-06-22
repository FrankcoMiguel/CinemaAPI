using CinemaAPI.Core.Entities;
using CinemaAPI.Core.Interfaces;
using CinemaAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaAPIContext _context;

        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<Crew> _crewRepository;
        private readonly IRepository<CrewMember> _crewMemberRepository;
        private readonly IRepository<CrewRole> _crewRoleRepository;
        private readonly IRepository<Director> _directorRepository;
        private readonly IRepository<Film> _filmRepository;


        public UnitOfWork(CinemaAPIContext context)
        {
            _context = context;
        }

        public IRepository<Actor> ActorRepository => _actorRepository ?? new BaseRepository<Actor>(_context);

        public IRepository<Crew> CrewRepository => _crewRepository ?? new BaseRepository<Crew>(_context);

        public IRepository<CrewMember> CrewMemberRepository => _crewMemberRepository ?? new BaseRepository<CrewMember>(_context);

        public IRepository<CrewRole> CrewRoleRepository => _crewRoleRepository ?? new BaseRepository<CrewRole>(_context);

        public IRepository<Director> DirectorRepository => _directorRepository ?? new BaseRepository<Director>(_context);

        public IRepository<Film> FilmRepository => _filmRepository ?? new BaseRepository<Film>(_context);

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
