using Microsoft.AspNetCore.Mvc;
using SkyTrack.Api.Services;

namespace SkyTrack.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController(SensorService sensorService) : ControllerBase
    {

        [HttpGet]
        public ActionResult GetTemperature()
        {
            var temperature = sensorService.GetTemperature();
            return Ok(temperature);
        }
    }
}
