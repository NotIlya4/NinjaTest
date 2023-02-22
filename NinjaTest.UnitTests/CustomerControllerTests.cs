using TestNinja.Fundamentals;

namespace NinjaTest.Test;

public class CustomerControllerTests
{
    private CustomerController _customerController = null;

    [SetUp]
    public void SetUp()
    {
        _customerController = new();
    }
    
    [Test]
    public void GetCustomer_IdIsZero_ReturnNotFound()
    {
        var result = _customerController.GetCustomer(0);
        
        Assert.That(result, Is.TypeOf<NotFound>());
    }

    [Test]
    public void GetCustomer_IdIsNotZero_ReturnOk()
    {
        var result = _customerController.GetCustomer(1);
        
        Assert.That(result, Is.TypeOf<Ok>());
    }
}