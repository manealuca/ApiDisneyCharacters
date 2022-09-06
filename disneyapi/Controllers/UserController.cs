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
    public class UserController : ControllerBase
    {
        [Route("auth/Register")]
        [HttpPost]
        public IActionResult Postuser(UserEntity user)
        {
            using ( var db = new DisneyContext())
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
                return Ok(user);
        }
        [Route("auth/Login")]
        [HttpGet]
        public IActionResult Getuser(string userName, string password)
        {
            using (var db = new DisneyContext())
            {
                var user = db.Users.Where(u => ((u.UserName == userName || u.Email == userName) && u.Password == password)).FirstOrDefault();
            }
                return Ok();
        }

    }
}
