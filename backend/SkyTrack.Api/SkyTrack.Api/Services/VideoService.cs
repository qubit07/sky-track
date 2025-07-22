using SkyTrack.Api.Dtos;

namespace SkyTrack.Api.Services
{
    public class VideoService(ILogger<VideoService> logger)
    {
        private readonly List<string> _videos = new List<string> { "Video1.mp4", "Video2.mp4", "Video3.mp4" };

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
            return _videos;
        }

        public string GetVideoById(int id)
        {
            // Logic to retrieve a video by its ID
            // This could involve fetching from a database or an in-memory collection
            // Placeholder for actual video retrieval logic
            if (id < 0 || id >= _videos.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Video ID is out of range.");
            }
            return _videos[id];
        }

        public void DeleteVideo(int id)
        {
            // Logic to delete a video by its ID
            // This could involve removing from a database or an in-memory collection
            if (id < 0 || id >= _videos.Count)
            {
                logger.LogWarning($"Video with ID {id} not found for deletion.");
                return;
            }
            _videos.RemoveAt(id);
            logger.LogInformation($"Video with ID {id} deleted successfully.");
        }

        public async Task CleanupVideos()
        {
            // Logic to clean up old or unnecessary videos
            await Task.Delay(1000); // Simulate async operation

            logger.LogInformation("Video cleanup operation completed successfully.");
        }
    }
}
