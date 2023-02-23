using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace NinjaTest.Mocking
{
    public class VideoService
    {
        private readonly IFileReader _fileReader;
        private readonly IVideoRepository _videoRepository;

        public VideoService(IFileReader fileReader, IVideoRepository videoRepository)
        {
            _fileReader = fileReader;
            _videoRepository = videoRepository;
        }
        
        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public async Task<string> GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            
            var videos = await _videoRepository.GetUnprocessedVideos();
                
            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public interface IVideoRepository
    {
        Task<List<Video>> GetUnprocessedVideos();
    }
    
    public class VideoRepository : IVideoRepository
    {
        private readonly VideoContext _context;

        public VideoRepository(VideoContext context)
        {
            _context = context;
        }
        
        public async Task<List<Video>> GetUnprocessedVideos()
        {
            return await (from video in _context.Videos
                where !video.IsProcessed
                select video).ToListAsync();
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}