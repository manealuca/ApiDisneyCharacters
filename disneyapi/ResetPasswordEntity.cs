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
    public class ResetPasswordEntity
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [MinLength(6), MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [MinLength(6, ErrorMessage = "La contraseña debe tener 6 caracteres como minimo"), Compare("Password")]
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
