namespace NinjaTest.Fundamentals;

public class Reservation
{
    public User? MadeBy { get; set; }

    public bool CanBeCancelledBy(User user)
    {
        return (user.IsAdmin || MadeBy == user);
    }
        
}

public class User
{
    public required bool IsAdmin { get; set; }
}