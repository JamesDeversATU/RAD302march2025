using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad302feCL2025
{
    internal interface IGenreRepository
    {

        Task<List<Genre>> GetAllGenresAsync();

        Task<Genre> GetGenreByIdAsync(int id);

        Task AddGenreAsync(Genre genre);

        Task UpdateGenreAsync(Genre genre);

        Task DeleteGenreAsync(int id);

        Task<List<Movie>> GetMoviesByGenreIdAsync(int genreId);

        Task<List<Actor>> GetActorsByGenreIdAsync(int genreId);


    }
}
