using Microsoft.AspNetCore.Mvc;
using SkyTrack.Api.Dtos;
using SkyTrack.Api.Services;

namespace SkyTrack.Api.Controllers
{
    public class NotificationController(NotificationService notificationService) : BaseController
    {

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterDevice([FromBody] NotificationRegisterDto dto)
        {
            notificationService.RegisterDevice(dto);
            return Ok(new { message = "Device registered successfully." });

        }

        [HttpPost("unregister")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveDevice([FromBody] NotificationUnregisterDto dto)
        {
            notificationService.UnregisterDevice(dto);
            return Ok(new { message = "Device unregistered successfully." });
        }

        [HttpGet("devices")]
        public ActionResult GetDevices()
        {
            var devices = notificationService.GetRegisteredDevices();
            return Ok(devices);
        }

    }
}
