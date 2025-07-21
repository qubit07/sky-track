namespace SkyTrack.Api.Dtos
{
    public class NotificationUnregisterDto
    {
        public required string UserId { get; set; }
        public required string DeviceToken { get; set; }
    }
}
