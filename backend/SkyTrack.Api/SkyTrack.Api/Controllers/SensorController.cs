using Microsoft.AspNetCore.Mvc;
using SkyTrack.Api.Services;

namespace SkyTrack.Api.Controllers
{
    public class SensorController(SensorService sensorService) : BaseController
    {

        [HttpGet]
        public ActionResult GetTemperature()
        {
            var temperature = sensorService.GetTemperature();
            return Ok(temperature);
        }
    }
}
