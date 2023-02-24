using System.Linq.Expressions;
using Moq;
using NinjaTest.Mocking;
using Moq.Protected;

namespace NinjaTest.Test.Mocking;

public class EmployeeControllerTests
{
    private Mock<IEmployeeRepository> _employeeRepository = null!;
    private EmployeeController _employeeController = null!;
    
    [SetUp]
    public void SetUp()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();
        _employeeController = new EmployeeController(_employeeRepository.Object);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void DeleteEmployee_IdProvided_CallRepositoryRemoveWithId(int id)
    {
        Expression<Func<IEmployeeRepository, Task>> expression = er => er.RemoveById(id);
        _employeeRepository.Setup(expression);

        _employeeController.DeleteEmployee(id);

        Assert.That(() => { _employeeRepository.Verify(expression, Times.Once);}, Throws.Nothing);
    }
}