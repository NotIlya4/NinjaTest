using NinjaTest.Fundamentals;

namespace NinjaTest.Test.Fundamentals;

public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator _calculator = null!;
    
    [SetUp]
    public void SetUp()
    {
        _calculator = new();
    }

    [Test]
    [TestCase(301)]
    [TestCase(-1)]
    public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => { _calculator.CalculateDemeritPoints(speed); },
            Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }
    
    [Test]
    [TestCase(0, 0)]
    [TestCase(20, 0)]
    [TestCase(65, 0)]
    [TestCase(66, 0)]
    [TestCase(69, 0)]
    [TestCase(70, 1)]
    [TestCase(75, 2)]
    public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectation)
    {
        int result = _calculator.CalculateDemeritPoints(speed);
        
        Assert.That(result, Is.EqualTo(expectation));   
    }
}