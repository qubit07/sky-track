namespace SkyTrack.Api.Services
{
    public class SimulationService(ILogger<SimulationService> logger)
    {
        public void StartSimulation()
        {
            // Logic to start the simulation
            logger.LogInformation("Simulation started.");
        }

        public void StopSimulation()
        {
            // Logic to stop the simulation
            logger.LogInformation("Simulation stopped.");
        }

    }
}
