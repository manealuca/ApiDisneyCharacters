using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
namespace disneyapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CharacterMovieController : ControllerBase
    {
        [Route("/Character/Movies")]
        [HttpPost]
        public IActionResult PostCharacterMovie(CharacterMovieEntity characterMovie)
        {
            CharacterEntity oCharacter = new CharacterEntity();
            using (var db = new DisneyContext())
            {
                try
                {
                    oCharacter = db.Characters.Where(cm => cm.CharacterId == characterMovie.CharactersCharacterId).Include(cm => cm.Movies).FirstOrDefault();
                    if (oCharacter == null) return NotFound();

                    var oMovie = db.Movies.Find(characterMovie.MoviesMovieId);
                    if (oMovie == null) return NotFound();
                    db.CharacterEntityMovieEntity.Add(characterMovie);
                    //oCharacter.Movies.Add(oMovie);

                    db.SaveChanges();
                    return Ok(oCharacter);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Ok(oCharacter);
        }
        [HttpGet]
        [AllowAnonymous]
        public List<MovieEntity> GetMovies(int id)
        {
            using (var db = new DisneyContext())
            {
                //List<MovieEntity> Movies = db.characterEntityMovieEntities.Where(cm=>cm.CharacterId == id).Include(cm=>cm.Movie).ToList();
                var CharacterMovies = db.CharacterEntityMovieEntity.Where(cm => cm.CharactersCharacterId == id).Include(cm => cm.Movie).ToList();
                List<MovieEntity> Movies = new List<MovieEntity>();
                foreach(var cm in CharacterMovies)
                {
                    Movies.Add(cm.Movie);
                }
                if (Movies == null) return null;

                return Movies;
            }
               
        }
    [Route("/CharacterCharacter-")]
    [HttpGet,AllowAnonymous]
    public List<CharacterEntity> GetCharcters(int id)
        {
            List<CharacterEntity> characters = new List<CharacterEntity>();
            using (var db = new DisneyContext())
            {
                try
                {
                    //var characterMovies = (from cm in db.CharacterEntityMovieEntity where cm.MoviesMovieId == id select cm.Character).ToList();
                    var characterMovies = db.CharacterEntityMovieEntity.Where(cm => cm.MoviesMovieId == id).ToList();
                    
                    foreach(var cm in characterMovies)
                    {
                        CharacterEntity aux = db.Characters.Find(cm.CharactersCharacterId);
                        characters.Add(aux);
                        
                        if (characters == null) return null;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

                return characters;
        }
        
    }
}
