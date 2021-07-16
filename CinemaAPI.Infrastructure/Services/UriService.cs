using CinemaAPI.Core.QueryFilters;
using CinemaAPI.Infrastructure.Interfaces;
using System;

namespace CinemaAPI.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }


        public Uri GetGenrePaginationUri(GenreQueryFilter filter, string actionURL)
        {
            string baseURL = $"{_baseUri}{actionURL}";
            return new Uri(baseURL);
        }

        public Uri GetFilmPaginationUri(FilmQueryFilter filter, string actionURL)
        {
            string baseURL = $"{_baseUri}{actionURL}";
            return new Uri(baseURL);
        }

    }
}
