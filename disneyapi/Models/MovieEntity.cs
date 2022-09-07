using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using APIDisneyCharacter.Controllers;

namespace disneyapi.Models
{
    [BindProperties]
    [Serializable]
    public class MovieEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        [Required]
        [StringLength(50)]
        public string MovieName { get; set; } = string.Empty;
        //[Required]
        public byte[]? MovieImage { get; set; }
        [Required]
        public DateTime? MovieCreationDate { get; set; }

        [Required]
        [Range(0.0, 5.0)]
        public decimal? MovieScore { get; set; }


        //TODO CONECCION A CHARACTERS Y CATEGORY
        //[Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public CategoryEntity? Category { get; set; }
        [JsonIgnore]
        public List<CharacterEntity>? Characters { get; set; }
        // public ICollection<CharacterMovieEntity> CharactersMovie { get; set; }


    }
}
