using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [BindProperties]
    public class CharacterEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }
        // [Required]
        public byte[]? Image { get; set; }
        [Required]
        [StringLength(50)]
        public string CharacterName { get; set; } = string.Empty;
        [Required]
        public int CharacetrAge { get; set; }
        [Required]
        public float CharacterWeight { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        [MaxLength]
        public string CharacterHistory { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual List<MovieEntity>? Movies { get; set; }
        //public ICollection<CharacterMovieEntity> CharacterMovie { get; set; }
        //TODO CONECCION A MOVIES
    }
}