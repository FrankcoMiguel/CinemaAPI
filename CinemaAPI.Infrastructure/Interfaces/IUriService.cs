using CinemaAPI.Core.QueryFilters;
using System;

namespace CinemaAPI.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetGenrePaginationUri(GenreQueryFilter filter, string actionURL);
        Uri GetFilmPaginationUri(FilmQueryFilter filter, string actionURL);

    }
}
