namespace SkyTrack.Api.Dtos
{
    public class NotificationRegisterDto
    {
        public required string UserId { get; set; }
        public required string DeviceToken { get; set; }
        public required string DeviceType { get; set; } // e.g., "Android", "iOS", "Web"

    }
}
