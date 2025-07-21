using Microsoft.AspNetCore.Mvc;
using SkyTrack.Api.Dtos;
using SkyTrack.Api.Services;

namespace SkyTrack.Api.Controllers
{
    public class VideoController(VideoService videoService) : BaseController
    {


        [HttpPost("mark")]
        [ValidateAntiForgeryToken]
        public ActionResult MarkVideo(MarkVideoDto dto)
        {
            videoService.MarkVideo(dto);
            return Ok(new { message = "Video marked successfully." });
        }

        [HttpGet]
        public ActionResult GetVideos()
        {
            var videos = videoService.GetVideos();
            return Ok(videos);
        }

        public ActionResult GetVideo(int id)
        {
            var video = videoService.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }


        [HttpDelete("delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVideo(int id)
        {
            videoService.DeleteVideo(id);
            return Ok(new { message = "Video deleted successfully." });
        }
    }
}
