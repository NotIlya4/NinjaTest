using System.Net;
using Moq;
using NinjaTest.Mocking;

namespace NinjaTest.Test.Mocking;

public class InstallerHelperTests
{
    private Mock<IFileDownloader> _fileDownloader = null!;
    private InstallerHelper _installerHelper = null!;

    [SetUp]
    public void SetUp()
    {
        _fileDownloader = new Mock<IFileDownloader>();
        _installerHelper = new InstallerHelper(_fileDownloader.Object);
    }

    [Test]
    public void DownloadInstaller_DownloadFails_ReturnFalse()
    {
        _fileDownloader.Setup(
            fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<WebException>();

        var result = _installerHelper.DownloadInstaller("customer", "installer");
        
        Assert.False(result);
    }
    
    [Test]
    public void DownloadInstaller_DownloadCompletes_ReturnTrue()
    {
        var result = _installerHelper.DownloadInstaller("customer", "installer");
        
        Assert.True(result);
    }
}