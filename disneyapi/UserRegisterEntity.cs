using disneyapi.Controllers;
using Microsoft.AspNetCore.Mvc;
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
    public class UserRegisterEntity
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(70)]
        public string Email { get; set; }

        [Required]
        [MinLength(6),StringLength(30)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ResetPassword { get; set; }

    }
}
