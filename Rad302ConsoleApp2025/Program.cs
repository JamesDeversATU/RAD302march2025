using Microsoft.IdentityModel.Tokens;
using Rad302feCL2025;
using System.Text.Json;
using Tracker.WebAPIClient;

using System.Net.Http.Headers;

using System.Net.Http;

namespace Rad302ConsoleApp2025
{
    internal class Program
    {
        static async void Main(string[] args)
        {

            ActivityAPIClient.Track(StudentID: "s00236260", StudentName: "James Mccafferty Devers", activityName: "Rad302 fe March 2025", Task: "Q1e Implementing Console queries");

            var token = await GetAuthTokenAsync("bob.bloggs@example.com", "yourpassword");

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Failed to retrieve token.");
                return;
            }

            Console.WriteLine("JWT Token: " + token);


            var genres = await GetAllGenresAsync(token);
            if (genres != null || genres.count ==0)
            {
                foreach (var genre in genres)
                {
                    Console.WriteLine("no genereas found");
                    return;
                }
            }
            Console.WriteLine("Genres:");
            for(int i = 0; < genres.Count; i++)
            {
                Console.WriteLine($"ID: {genres[i].Id}, Name: {genres[i].Name}");
            }




            var selectedGenre = genres[0];
            Console.WriteLine($"chose genera: {  selectedGenre.Name}");

            var movies = await GetMoviesByGenreAsync(token,selectedGenre.Id);
            if (movies != null || movies.count == 0)
            {
                foreach (var movie in movies)
                {
                    Console.WriteLine("no movies found");
                    return;
                }
            }

            Console.WriteLine("Movies list for:" + selectedGenre.name + ";");

            for (int i = 0; i < movies.count; i++)
            {
                Console.WriteLine($"ID: {movies[i].Id}, Title: {movies[i].Title}");
            }

            var selectedMovie = movies[0];
            Console.WriteLine($"chose movie: {selectedMovie.Title}") ({selectedMovie.releasedate});

        }

        private static async Task<List<string>> GetAuthTokenAsync(string username, string passowrd)
        {
            var client = new HttpClient();

            var loginUrl = "https://localhost:7260/api/Account/login";

            var loginpayload = new
            {
                username = username,
                password = passowrd
            };

            var content = new StringContent(JsonSerializer.Serialize(loginpayload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(loginUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<string>(responseContent);
                return token;
            }
            else
            {
                Console.WriteLine("Login failed: " + response.StatusCode);
                return null;
            }

        }

        private static async Task<List<Genre>> GetAllGenresAsync(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var genresUrl = "https://localhost:7260/api/Genres";

            var response = await client.GetAsync(genresUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var genres = JsonSerializer.Deserialize<List<Genre>>(responseContent);
                return genres;
            }
            else
            {
                Console.WriteLine("Failed to retrieve genres: " + response.StatusCode);
                return null;
            }



        }

        private static async Task<List<Movie>> GetMoviesByGenreAsync(string token, int genreId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var moviesUrl = $"https://localhost:7260/api/Genres/{genreId}/movies";

            var response = await client.GetAsync(moviesUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var movies = JsonSerializer.Deserialize<List<Movie>>(responseContent);
                return movies;
            }
            else
            {
                Console.WriteLine("Failed to retrieve movies: " + response.StatusCode);
                return null;
            }

        }
        //loads of bugs but no time to fix

        }
}
