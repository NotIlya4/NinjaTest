using Math = NinjaTest.Fundamentals.Math;

namespace NinjaTest.Test;

public class MathTests
{
    private Math _math = null!;
    
    [SetUp]
    public void SetUp()
    {
        _math = new();
    }
    
    [Test]
    public void Add_WhenCalled_ReturnSumOfArguments()
    {
        int result = _math.Add(1, 2);

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    [TestCase(2, 1 ,2)]
    [TestCase(1, 2, 2)]
    [TestCase(1, 1, 1)]
    public void Max_WhenCalled_ReturnGraterArgument(int a, int b, int expectedResult)
    {
        int result = _math.Max(a, b);
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void GetOddNumber_LimitIsPositive_ReturnOddNumbersUpToLimit()
    {
        IEnumerable<int> result = _math.GetOddNumbers(5);
        
        Assert.That(result, Is.EquivalentTo(new [] {1,3,5}));
    }
}