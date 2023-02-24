using Moq;
using NinjaTest.Mocking;

namespace NinjaTest.Test.Mocking;

public class BookingHelper_OverlappingBookingExistTests
{
    private Booking _existingBooking = null!;
    private Mock<IBookingRepository> _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _existingBooking = new Booking()
        {
            Id = 2,
            ArrivalDate = ArriveOn(2017, 1, 15),
            DepartureDate = DepartOn(2017, 1, 20),
            Reference = "a"
        };
        
        _repository = new Mock<IBookingRepository>();
        _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>()
        {
            _existingBooking
        }.AsQueryable());
    }
    
    [Test]
    public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        string result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate, 2),
            DepartureDate = Before(_existingBooking.ArrivalDate)
        }, _repository.Object);
        
        Assert.That(result, Is.EqualTo(""));
    }
    
    [Test]
    public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
    {
        string result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.ArrivalDate)
        }, _repository.Object);
        
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }
    
    [Test]
    public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
    {
        string result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate)
        }, _repository.Object);
        
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }
    
    [Test]
    public void BookingStartsAndFinishesInMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
    {
        string result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.ArrivalDate),
            DepartureDate = Before(_existingBooking.DepartureDate)
        }, _repository.Object);
        
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }
    
    [Test]
    public void BookingStartsInMiddleAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
    {
        string result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate)
        }, _repository.Object);
        
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }
    
    [Test]
    public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
    {
        string result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.DepartureDate),
            DepartureDate = After(_existingBooking.DepartureDate, 2)
        }, _repository.Object);
        
        Assert.That(result, Is.EqualTo(""));
    }
    
    [Test]
    public void BookingsOverlapButNewBookingCancelled_ReturnEmptyString()
    {
        string result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate),
            Status = "Cancelled"
        }, _repository.Object);
        
        Assert.That(result, Is.EqualTo(""));
    }

    private DateTime Before(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(-days);
    }

    private DateTime After(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(days);
    }
    
    private DateTime ArriveOn(int year, int mouth, int day)
    {
        return new DateTime(year, mouth, day, 14, 0, 0);
    }
    
    private DateTime DepartOn(int year, int mouth, int day)
    {
        return new DateTime(year, mouth, day, 10, 0, 0);
    }
}