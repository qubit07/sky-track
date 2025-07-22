namespace SkyTrack.Api.Models
{
    public class CleanupOptions
    {
        public bool Enabled { get; set; }
        public int IntervalHours { get; set; }
        public int RetentionDays { get; set; }
    }
}
