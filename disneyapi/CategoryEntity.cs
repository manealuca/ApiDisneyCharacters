﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace disneyapi
{
    [Serializable]
    public class CategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [StringLength(50)]
        public string? CategoryName { get; set; } = String.Empty;
        [JsonIgnore]
        public byte[]? CategoryImage { get; set; }
        [JsonIgnore]
        public List<MovieEntity>? Movies { get; set; }

    }
}