using NinjaTest.Fundamentals;

namespace NinjaTest.Test.Fundamentals;

[TestFixture]
public class ErrorLoggerTests
{
    private ErrorLogger _errorLogger = null!;
    
    [SetUp]
    public void SetUp()
    {
        _errorLogger = new();
    }

    [Test]
    public void Log_WhenCalled_SetLastErrorProperty()
    {
        _errorLogger.Log("a");
        
        Assert.That(_errorLogger.LastError, Is.EqualTo("a"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Log_InvalidError_ThrowArgumentNullException(string? error)
    {
        Assert.That(() => _errorLogger.Log(error), Throws.ArgumentNullException);
    }

    [Test]
    public void Log_ValidError_RaiseErrorLoggedEvent()
    {
        Guid id = Guid.Empty;
        _errorLogger.ErrorLogged += (sender, guid) => { id = guid; };
        
        _errorLogger.Log("a");
        
        Assert.That(id, Is.Not.EqualTo(Guid.Empty));
    }
}