using Moq;
using NinjaTest.Mocking;

namespace NinjaTest.Test.Mocking;

public class VideoServiceTests
{
    private Mock<IFileReader> _fileReader = null!;
    private Mock<IVideoRepository> _videoRepository = null!;
    private VideoService _videoService = null!;
    private List<Video> _videos = null!;

    [SetUp]
    public void SetUp()
    {
        _fileReader = new Mock<IFileReader>();
        _videoRepository = new Mock<IVideoRepository>();
        _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
        _videos = new()
        {
            new Video() { Id = 1 },
            new Video() { Id = 2 },
            new Video() { Id = 3 },
        };
    }
    
    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public async Task GetUnprocessedVideosAsCsv_NoVideos_ReturnBlankString()
    {
        _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).ReturnsAsync(new List<Video>());

        var result = await _videoService.GetUnprocessedVideosAsCsv();
        
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public async Task GetUnprocessedVideosAsCsv_ListVideos_IdsSeparatedByComma()
    {
        _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).ReturnsAsync(_videos);

        var result = await _videoService.GetUnprocessedVideosAsCsv();
        
        Assert.That(result, Is.EqualTo("1,2,3"));
    }
}