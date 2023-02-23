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
}