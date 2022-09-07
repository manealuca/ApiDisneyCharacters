using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace disneyapi.Models
{
    [Serializable]
    public class CharacterMovieEntity
    {
        [Key]
        public int CharactersCharacterId { get; set; }
        [Key]
        public int MoviesMovieId { get; set; }


        //[ForeignKey("CharacterId")]
        [JsonIgnore]
        public virtual CharacterEntity? Character { get; set; }

        // [ForeignKey("MovieId")]
        [JsonIgnore]
        public virtual MovieEntity? Movie { get; set; }


    }

}
