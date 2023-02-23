using NinjaTest.Fundamentals;

namespace NinjaTest.Test.Fundamentals;

public class FizzBuzzTests
{
    [Test]
    public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz()
    {
        string result = FizzBuzz.GetOutput(3 * 5);
        
        Assert.That(result, Is.EqualTo("FizzBuzz"));
    }
    
    [Test]
    public void GetOutput_InputIsDivisibleBy3Only_ReturnFizz()
    {
        string result = FizzBuzz.GetOutput(3);
        
        Assert.That(result, Is.EqualTo("Fizz"));
    }
    
    [Test]
    public void GetOutput_InputIsDivisibleBy5Only_ReturnBuzz()
    {
        string result = FizzBuzz.GetOutput(5);
        
        Assert.That(result, Is.EqualTo("Buzz"));
    }
    
    [Test]
    public void GetOutput_InputIsNotDivisibleBy3Nor5_ReturnNumberInString()
    {
        string result = FizzBuzz.GetOutput(2);
        
        Assert.That(result, Is.EqualTo("2"));
    }
}