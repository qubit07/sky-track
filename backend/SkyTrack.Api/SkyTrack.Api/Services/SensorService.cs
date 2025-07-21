namespace SkyTrack.Api.Services
{
    public class SensorService
    {
        public int GetTemperature()
        {
            // Simulate getting temperature from a sensor
            return Random.Shared.Next(-20, 55); // Example range for temperature in Celsius
        }
    }
}
