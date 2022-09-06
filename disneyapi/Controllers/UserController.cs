﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace disneyapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        /*public static UserEntity user = new UserEntity();
        private readonly IConfiguration configuration;
        public UserController(IConfiguration oConfiguration)
        {
            configuration = oConfiguration;
        }*/

        [Route("/auth/Register")]
        [HttpPost]
        public IActionResult Register(UserRegisterEntity user)
        {
            using (var db = new DisneyContext())
            {
                try
                {
                    if (db.Users.Any(u => u.Email == user.Email || u.UserName == user.UserName))
                    {
                        return BadRequest("El usuario ya esta registrado");
                    }
                    CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    var newUser = new UserEntity
                    {
                        Email = user.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        VerificationToken = CreateRandomToken(),
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return Ok(newUser);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            return BadRequest();
        }

           private string CreateRandomToken()
            {
                return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            }
            private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
            {
                using (var hmac = new HMACSHA512()) {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }
        private bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                
                var computeddHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeddHash.SequenceEqual(passwordHash);
            }
        }

        /*private string CreateToken()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettiengs:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims:claims,expires:DateTime.Now.AddDays(1),signingCredentials:credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }*/
        [Route("auth/Login")]
        [HttpGet]
            public IActionResult Login(string userName, string password)
            {
                using (var db = new DisneyContext())
                {
                    var user = db.Users.Where(u => ((u.UserName == userName || u.Email == userName))).FirstOrDefault();
                    if(user == null)
                {
                    return BadRequest("El usuario no existe");
                }
                    if(user.VerifiedAt == null)
                {
                    return BadRequest("La cuenta no esta verificada");
                }
                if (!VerifyPasswordHash(password, user.PasswordHash,user.PasswordSalt))
                {
                    return BadRequest("La contraseña es incorrecta");
                }
                
            }
                return Ok($"Inicio  de sesion exitoso, {userName} !");
            }
        [Route("auth/Verify")]
        [HttpGet]
        public IActionResult VerifyUser(string token)
        {
            using (var db = new DisneyContext())
            {
                var user = db.Users.FirstOrDefault(u=>u.VerificationToken==token);
                if (user == null)
                {
                    return BadRequest("Token invalido");
                }
                user.VerifiedAt = DateTime.Now;
                db.SaveChanges();

            }
            return Ok("Usuario validado exitosamente!");
        }
        [Route("auth/ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPassword(string email)
        {
            using (var db = new DisneyContext())
            {
                var user = db.Users.Where(u=> u.Email==email).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest("Usuario no encontrado");
                }
                user.ResetVerificationToken = CreateRandomToken();
                user.ResetTokenExpire = DateTime.Now.AddDays(1);
                db.SaveChanges();

            }
            return Ok("Puedes crear una nueva contraseña ahora");
        }
        [Route("auth/ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordEntity resetPassword)
        {
            using (var db = new DisneyContext())
            {
                var user = db.Users.Where(u => u.ResetVerificationToken == resetPassword.Token).FirstOrDefault();
                if (user == null || user.ResetTokenExpire <DateTime.Now)
                {
                    return BadRequest("Usuario no encontrado");
                }
                CreatePasswordHash(resetPassword.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.ResetVerificationToken = null;
                user.ResetTokenExpire = null;
                db.SaveChanges();
                return Ok("Contraseña modificada exitosamente");
            }
            return Ok("Puedes crear una nueva contraseña ahora");
        }

        [HttpGet]
            public List<UserEntity> GetUsers()
            {
                using (var db = new DisneyContext())
                {
                    return db.Users.ToList();
                }
            }

        }
    } 

