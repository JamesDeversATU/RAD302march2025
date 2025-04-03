using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad302feCL2025
{
    public interface IMovieRepository
    {

        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> AddMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int id);
        Task<List<Actor>> GetActorsByMovieIdAsync(int movieId);
        Task<List<Genre>> GetGenresByMovieIdAsync(int movieId);
    }
}
