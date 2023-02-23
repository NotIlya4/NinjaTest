using NinjaTest.Fundamentals;

namespace NinjaTest.Test.Fundamentals;

public class HtmlFormatterTests
{
    private HtmlFormatter _htmlFormatter = null!;
    
    [SetUp]
    public void SetUp()
    {
        _htmlFormatter = new HtmlFormatter();
    }
    
    [Test]
    public void FormatAsBold_WhenCalled_EncloseStringWithStrongTag()
    {
        string result = _htmlFormatter.FormatAsBold("abc");
        
        Assert.That(result, Is.EqualTo("<strong>abc</strong>"));
    }
}