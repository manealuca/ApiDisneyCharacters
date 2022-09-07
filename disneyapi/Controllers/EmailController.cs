﻿using disneyapi.Models;
using disneyapi.Services.EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace disneyapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public IActionResult SendEmail(EmailDto email)
        {
            _emailService.SendEmail(email);
            return Ok("Correo enviado exitosamente");
        }
    }
}
