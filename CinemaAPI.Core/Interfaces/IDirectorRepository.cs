﻿using CinemaAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAPI.Core.Interfaces
{
    public interface IDirectorRepository
    {
        Task<IEnumerable<Director>> GetDirectors();
        Task<Director> GetDirector(int id);
    }
}