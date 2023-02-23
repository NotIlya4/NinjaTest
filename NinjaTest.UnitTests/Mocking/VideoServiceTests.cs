using Moq;
using NinjaTest.Mocking;

namespace NinjaTest.Test.Mocking;

public class VideoServiceTests
{
    private Mock<IFileReader> _fileReader = null!;
    private VideoService _videoService = null!;
    
    [SetUp]
    public void SetUp()
    {
        _fileReader = new Mock<IFileReader>();
        _videoService = new VideoService(_fileReader.Object);
    }
    
    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}