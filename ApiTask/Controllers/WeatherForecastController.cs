using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Models;
using ApiTask.Security;
using Microsoft.Extensions.Configuration;

namespace ApiTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
       // private readonly IConfiguration _config;
        private readonly IJWTSecurity _jwt;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJWTSecurity jwt)
        {
            _logger = logger;
           // _config = config;
            _jwt = jwt; 
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetToken")]
        public IActionResult  GetToken()
        {
            var user = new AppUser
            {
                LastName = "Edun",
                FirstName = "Olukunle",
                Email = "favourblessing@gmail.com",
                Role = "Decadev"

            };
            //var JwtGen = new JWTSecurity(_config);
           // return JwtGen.JWTGen(); 
           var token = _jwt.JWTGen(user);
           return Ok(token);
        }
    }
}
