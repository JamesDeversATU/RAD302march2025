using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad302feCL2025
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ActedIn> ActedIns { get; set; }

        public MovieContext()
        {

        }

        public MovieContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var myconnectionstring = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MovieDb-2025-S00236260";
            optionsBuilder.UseSqlServer(myconnectionstring)
              .LogTo(Console.WriteLine,
                     new[] { DbLoggerCategory.Database.Command.Name },
                     LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Avengers:Age of Ultron", ReleaseDate = 2015 },
                new Movie { Id = 2, Title = "The Judge", ReleaseDate = 1999 },
                new Movie { Id = 3, Title = "The Avengers", ReleaseDate = 2014 }
            );

            modelBuilder.Entity<Actor>().HasData(
              new Actor {  Id = 1 , FirstName = "Robert" , SecondName = "Downey.jr" },
              new Actor { Id = 2, FirstName = "Chris", SecondName = "hemsorth" }

           );

            modelBuilder.Entity<Genre>().HasData(
              new Genre { Id = 1, Name = "action" },
              new Genre { Id = 2, Name = "drama" }

           );

            modelBuilder.Entity<ActedIn>().HasData(
              new ActedIn { ActorId = 1, MovieId = 1 },
              new ActedIn { ActorId = 2, MovieId = 1 },
              new ActedIn { ActorId = 3, MovieId = 1 },
              new ActedIn { ActorId = 4, MovieId = 2 },
              new ActedIn { ActorId = 5, MovieId = 2 }
           );


        }
    }
}


//using the movie context class create a datavase called movie-DB-S00236260 
//with the database createed seed the data.