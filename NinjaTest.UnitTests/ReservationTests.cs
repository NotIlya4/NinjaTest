using TestNinja.Fundamentals;

namespace NinjaTest.Test;

public class ReservationTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CanBeCancelledBy_UserAdmin_ReturnsTrue()
    {
        Reservation reservation = new();

        bool result = reservation.CanBeCancelledBy(new() {IsAdmin = true});
        
        Assert.IsTrue(result);
    }

    [Test]
    public void CanBeCancelledBy_UserAuthorOfReservationNotAdmin_ReturnsTrue()
    {
        User user = new() { IsAdmin = false };
        Reservation reservation = new() { MadeBy = user };

        bool result = reservation.CanBeCancelledBy(user);
        
        Assert.True(result);
    }

    [Test]
    public void CanBeCancelledBy_UserNotAuthorOfReservationNotAdmin_ReturnsFalse()
    {
        Reservation reservation = new();

        bool result = reservation.CanBeCancelledBy(new User(){IsAdmin = false});
        
        Assert.False(result);
    }
}