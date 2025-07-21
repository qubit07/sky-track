using SkyTrack.Api.Dtos;

namespace SkyTrack.Api.Services
{
    public class VideoService(ILogger<VideoService> logger)
    {
        public void MarkVideo(MarkVideoDto dto)
        {
            // Logic to mark the video based on the provided dto
            // This could involve updating a database or an in-memory collection
            // For example:
            // var video = _videoRepository.GetById(dto.VideoId)

            logger.LogInformation($"Video marked with mark: {dto.Mark}");
        }

        public IEnumerable<string> GetVideos()
        {
            // Logic to retrieve videos
            // This could involve fetching from a database or an in-memory collection
            // For example:
            return new List<string> { "Video1.mp4", "Video2.mp4", "Video3.mp4" };
        }

        public string GetVideoById(int id)
        {
            // Logic to retrieve a video by its ID
            // This could involve fetching from a database or an in-memory collection
            // For example:
            return $"Video{id}.mp4"; // Placeholder for actual video retrieval logic
        }

        public void DeleteVideo(int id)
        {
            // Logic to delete a video by its ID
            // This could involve removing from a database or an in-memory collection
            // For example:
            logger.LogInformation($"Video with ID {id} deleted successfully.");
        }
    }
}
