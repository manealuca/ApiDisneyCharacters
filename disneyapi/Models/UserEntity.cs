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

namespace disneyapi.Models
{
    [BindProperties]
    [Serializable]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(70)]
        public string Email { get; set; }

        public byte[] PasswordSalt { get; set; } = new byte[32];
        public byte[] PasswordHash { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? ResetVerificationToken { get; set; }
        public DateTime? ResetTokenExpire { get; set; }
    }
    /*   public class UserExistAttribute:ValidationAttribute
       {
          protected override ValidationResult IsValid(object value, ValidationContext validationContext)
           {
               UserController uController = new UserController();
               var     user = uController.GetUsers();
               string Email = (string)value;
               if(user.Where(u=>u.Email == Email).Count() > 0)
               {
                   return new ValidationResult("Ya Existe un usuario asociado a este correo");
               }
               return ValidationResult.Success;
           }
       }*/
}
