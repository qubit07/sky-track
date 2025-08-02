using Microsoft.AspNetCore.Mvc;
using SkyTrack.Api.Services;

namespace SkyTrack.Api.Controllers
{
    public class SystemController(SimulationService simulationService) : BaseController
    {
        [HttpPost("simulation/start")]
        [ValidateAntiForgeryToken]
        public ActionResult StartSimulation()
        {
            simulationService.StartSimulation();
            return Ok(new { message = "Simulation started successfully." });
        }


        [HttpPost("simulation/stop")]
        [ValidateAntiForgeryToken]
        public ActionResult StopSimulation()
        {
            simulationService.StopSimulation();
            return Ok(new { message = "Simulation stopped successfully." });
        }
    }
}
