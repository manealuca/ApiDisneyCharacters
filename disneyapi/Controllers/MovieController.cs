using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using disneyapi;
using disneyapi.Controllers;

namespace APIDisneyCharacter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MovieController : ControllerBase
    {

        [Route("/movies/validacion")]
        [HttpGet]
        public List<MovieEntity> GetValidationMovieList()
        {
            using (var db = new DisneyContext())
            {
                var movies = db.Movies.ToList();
                return movies;
            }
                
        }

        [Route("/movies")]
        [HttpGet]
        public IActionResult GetMovieList()
        {
            List<MovieDto> movieList = new List<MovieDto>();
            using (var db = new DisneyContext())
            {
                var movies = db.Movies.ToList();
                foreach(var movie in movies)
                {
                    movieList.Add(newMovieDto(movie));
                }
            }
                return Ok(movieList);
        }
        [Route("/movie")]
        [HttpPost]
        public IActionResult PostMovie(MovieEntity oMovie)
        {
            using (var db = new DisneyContext())
            {
                try
                {
                    //oMovie.Category = db.Category.Find(oMovie.CategoryId);
                    oMovie.Category = db.Category.Find(oMovie.CategoryId);
                    db.Movies.Add(oMovie);
                    db.SaveChanges();
                
                }catch(DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
                return Ok(oMovie);
        }
        [Route("/movie")]
        [HttpGet]
        public IActionResult SearchMovie([FromQuery] string? categoryName,[FromQuery] string? name, [FromQuery] string? order,[FromQuery] DateTime? creationDate= null)
        {
            List<MovieEntity> oMovie = new List <MovieEntity>();
            List<CharacterEntity> characters = new List<CharacterEntity>();
            CategoryEntity? category = new CategoryEntity();
            try
            {
                using (var db = new DisneyContext())
                {
                    if (order == "DESC")
                    {
                        //category = db.Category.Where(c => c.CategoryName == categoryName).FirstOrDefault();
                        oMovie = (from m in db.Movies where (String.IsNullOrEmpty(name) || m.MovieName == name) && (String.IsNullOrEmpty(categoryName) || m.Category.CategoryName == categoryName )select m).OrderByDescending(m => m.MovieCreationDate).ToList();
                        //oMovie = db.Movies.Where(m => m.CategoryId == category.CategoryId).ToList();
                    }
                        
                    else
                        oMovie = (from m in db.Movies where(String.IsNullOrEmpty(name) || m.MovieName == name) && (String.IsNullOrEmpty(categoryName) || m.Category.CategoryName == categoryName) select m).OrderBy(m=>m.MovieCreationDate).ToList();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(oMovie);
        }

        [Route("/movie")]
        [HttpPut]
        public IActionResult UpdateMovie(MovieEntity oMovie)
        {

            using(var db = new DisneyContext())
            {
                try
                {
                    db.Movies.Update(oMovie);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
                return Ok($"Pelicula/Serie: {oMovie.MovieName} actualizada correctamente");
        }
        [Route("/movie")]
        [HttpDelete]
        public IActionResult DeleteMovie(int id)
        {
            MovieEntity oMovie = new MovieEntity();
            try
            {
                using (var db = new DisneyContext())
                {
                   oMovie = db.Movies.Find(id);
                    db.Remove(oMovie);
                    db.SaveChanges();
                }
            }catch(DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok($"Pelicula/Serie: {oMovie.MovieName} eliminado correctamente");
        }

        [Route("/movie/detalle")]
        [HttpGet]
        public IActionResult GetMovieDetail(int id)
        {
            List<CharacterEntity> characters = new List<CharacterEntity>();
            MovieDtoCharacter movieDtoCharacter = new MovieDtoCharacter();
            CharacterMovieController cmController = new CharacterMovieController();
            try
            {
                using (var db = new DisneyContext())
                {
                    var oMovie = db.Movies.Where(m=>m.MovieId==id).Include(m=>m.Category).ToList();
                    characters = cmController.GetCharcters(id);
                    movieDtoCharacter = newMovieDtoCharacter(oMovie.FirstOrDefault(), characters);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok(movieDtoCharacter);
        }

        internal MovieDtoCharacter newMovieDtoCharacter(MovieEntity movie,List<CharacterEntity> characters)
        {
            MovieDtoCharacter movieDtoCharacter = new MovieDtoCharacter();
            try
            {
                movieDtoCharacter.MovieId = movie.MovieId;
                movieDtoCharacter.MovieName = movie.MovieName;
                movieDtoCharacter.MovieImage = movie.MovieImage;
                movieDtoCharacter.MovieCreationDate = movie.MovieCreationDate;
                movieDtoCharacter.MovieScore = movie.MovieScore;
                movieDtoCharacter.CategoryId = movie.CategoryId;
                movieDtoCharacter.Category = movie.Category;              
                movieDtoCharacter.Characters = characters;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return movieDtoCharacter;
        }
        internal MovieDto newMovieDto(MovieEntity movie)
        {
            MovieDto movieDto = new MovieDto();
            try
            {
                movieDto.MovieName = movie.MovieName;
                movieDto.MovieImage = movie.MovieImage;
                movieDto.MovieCreationDate = movie.MovieCreationDate;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return movieDto;
        }
    }

    internal class MovieDtoCharacter
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public byte[]? MovieImage { get; set; }
        public DateTime? MovieCreationDate { get; set; }
        public decimal? MovieScore { get; set; }
        public int? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }

        public List<CharacterEntity>? Characters { get; set; }
    }
    internal class MovieDto
    {
        public byte[] MovieImage { get; set; }
        public string MovieName { get; set; }
        public DateTime? MovieCreationDate { get; set; }
    }
}
