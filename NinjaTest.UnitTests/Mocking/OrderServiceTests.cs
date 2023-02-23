using Moq;
using NinjaTest.Mocking;

namespace NinjaTest.Test.Mocking;

public class OrderServiceTests
{
    [Test]
    public void PlaceOrder_WhenCalled_StoreTheOrder()
    {
        var storage = new Mock<IStorage>();
        var service = new OrderService(storage.Object);

        var order = new Order();
        service.PlaceOrder(order);
        
        storage.Verify(s => s.Store(order));
    }
}