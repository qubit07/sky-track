namespace SkyTrack.Api.Models
{
    public class CleanupOptions
    {
        public required bool Enabled { get; set; }
        public required int IntervalHours { get; set; }
        public required int RetentionDays { get; set; }
    }
}
