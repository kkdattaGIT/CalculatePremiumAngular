using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CalculatePremium.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        private static Dictionary<string, double> genderFactor = new Dictionary<string, double>()
        {
            {"M",1.2},
            {"F",1.1}
        };


        [HttpGet("premium/{name}/{dob}/{gender}")]
        public IActionResult getPremium(string name, string dob, string gender)
        {
            //Age * GenderFactor * 100
            var res = new DateTime();
            if (DateTime.TryParse(dob, out res))
            {
                var age = DateTime.Now - res;
                var ageYears = DateTime.MinValue.Add(age).Year - 1;
                if(ageYears>=18 && ageYears <=65)
                {
                    var Premium = ageYears * genderFactor[gender] * 100;
                    return Ok(Premium);
                }
                else
                {
                    return Ok("Age is not in the range");
                }
                
            }
            else
            {
                return BadRequest("Failed to calculate Premium");
            }
        }
        
}
}
