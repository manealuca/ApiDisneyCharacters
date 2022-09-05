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
    public class CharacterController : ControllerBase
    {

        internal CharacterDto newCharDto(CharacterEntity character)
        {
            CharacterDto characterDto = new CharacterDto();
            characterDto.Name = character.CharacterName;
            characterDto.Image = character.Image;

            return characterDto;
        }

        [Route("/characters")]
        [HttpGet]
        public IActionResult GetCharacterList()
        {
            List<CharacterDto> charList = new List<CharacterDto>();

            CharacterDto chardto;
            using (var db = new DisneyContext())
            {
                var characters = db.Characters.ToList();
                foreach (var character in characters)
                {
                    chardto = newCharDto(character);
                    charList.Add(chardto);
                }
            }
            return Ok(charList);
        }

        [Route("/Character")]
        [HttpPost]
        public IActionResult PostCharacter(CharacterEntity oCharacter)
        {
            using (var db = new DisneyContext())
            {
                try
                {
                    //string converted = oCharacter.Image.ToString().Replace('-', '+');
                    //oCharacter.Image = Convert.FromBase64String(converted);
                    // db.CharacterMovie.AddRange(oCharacter.CharacterMovies);
                    //db.SaveChanges();
                    db.Characters.Add(oCharacter);
                    db.SaveChanges();
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return Ok(oCharacter);
        }

        [Route("/Character")]
        [HttpPut]
        public IActionResult UpdateCharacter(CharacterEntity oCharacter)
        {

            using (var db = new DisneyContext())
            {
                try
                {
                    db.Characters.Update(oCharacter);
                    db.SaveChanges();
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Ok(oCharacter);
        }

        [Route("/Character")]
        [HttpDelete]
        public IActionResult DeleteCharacter(int id)
        {
            CharacterEntity oCharacter = new CharacterEntity();
            using (var db = new DisneyContext())
            {
                try
                {
                    oCharacter = db.Characters.Find(id);
                    db.Remove(oCharacter);
                    db.SaveChanges();
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Ok($"El personaje: {oCharacter.CharacterName} fue eliminado correctamente");
        }
        [Route("/Character/detalle")]
        [HttpGet]
        public IActionResult GetCharacterDetail(int id)
        {
            List<MovieEntity> movies = new List<MovieEntity>();
            //CharacterDtoMovie characterDto = new CharacterDtoMovie();
            CharacterEntity oCharacter = new CharacterEntity();
            CharacterMovieController CmController = new CharacterMovieController();
            CharacterDtoMovie CharacterDto = new CharacterDtoMovie();
            using (var db = new DisneyContext())
            {
                try
                {
                    var aux = db.Characters.ToList();
                    oCharacter = db.Characters.Find(id);
                    movies = CmController.GetMovies(id);
                    //oCharacter.Movies = movies;
                    CharacterDto = newCharacterDtoMovie(oCharacter,movies);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Ok(CharacterDto);


        }
        [Route("/Character")]
        [HttpGet]
        public IActionResult SearchCharacter([FromQuery] string? name, [FromQuery] int? age, [FromQuery] float? weight, [FromQuery]string?movie,[FromQuery]string?order)
        {
            List<CharacterEntity> characters = new List<CharacterEntity>();
            List<MovieEntity> movies = new List<MovieEntity>();
            using (var db = new DisneyContext())
            {
                try
                {
                    //characters = (from c in db.Characters where (String.IsNullOrEmpty(name) || c.CharacterName == name) && (String.IsNullOrEmpty(movie) || c.Movies.Find(movie) == categoryName) select m).OrderByDescending(m => m.MovieCreationDate).ToList();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

                return Ok();
        }

       internal CharacterDtoMovie newCharacterDtoMovie(CharacterEntity oCharacter, List<MovieEntity> movies)
        {
            CharacterDtoMovie characterDtoMovie = new CharacterDtoMovie();
            characterDtoMovie.CharacetrAge = oCharacter.CharacetrAge;
            characterDtoMovie.CharacterName = oCharacter.CharacterName;
            characterDtoMovie.Image = oCharacter.Image;
            characterDtoMovie.CharacterWeight = oCharacter.CharacterWeight;
            characterDtoMovie.CharacterHistory = oCharacter.CharacterHistory;
            /*foreach (var m in movies)
            {
                characterDtoMovie.Movies.Add(m);
            }*/
            characterDtoMovie.Movies = movies;

            return characterDtoMovie;
        }
    }

    internal class CharacterDto
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }

    }

    public class CharacterDtoMovie
    {

        public byte[]? Image { get; set; }

        public string CharacterName { get; set; }

        public int CharacetrAge { get; set; }

        public float CharacterWeight { get; set; }

        public string? CharacterHistory { get; set; }

        public List<MovieEntity>? Movies { get; set; }


    }

}
