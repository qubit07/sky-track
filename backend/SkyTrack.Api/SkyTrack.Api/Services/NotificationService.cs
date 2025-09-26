using SkyTrack.Api.Dtos;

namespace SkyTrack.Api.Services
{
    public class NotificationService(ILogger<NotificationService> logger)
    {
        private readonly List<NotificationRegisterDto> _registeredDevices = new List<NotificationRegisterDto>();

        public void RegisterDevice(NotificationRegisterDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.UserId) || string.IsNullOrEmpty(dto.DeviceToken))
            {
                throw new ArgumentException("Invalid device registration data.");
            }

            logger.LogInformation($"Registering device for user {dto.UserId}: Token={dto.DeviceToken}, Type={dto.DeviceType}");
            _registeredDevices.Add(dto);
        }
        public void UnregisterDevice(NotificationUnregisterDto dto)
        {
            var device = _registeredDevices.FirstOrDefault(d => d.UserId == dto.UserId && d.DeviceToken == dto.DeviceToken);
            if (device != null)
            {
                _registeredDevices.Remove(device);
                logger.LogInformation($"Unregistered device for user {dto.UserId}: Token={dto.DeviceToken}");
            }
            else
            {
                logger.LogWarning($"Device not found for user {dto.UserId}: Token={dto.DeviceToken}");
            }
        }
        public IEnumerable<NotificationRegisterDto> GetRegisteredDevices()
        {
            logger.LogWarning($"Get Devices called. Total registered devices: {_registeredDevices.Count}");
            return _registeredDevices;
        }

    }
}
