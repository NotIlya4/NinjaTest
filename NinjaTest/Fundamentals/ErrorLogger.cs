
namespace NinjaTest.Fundamentals
{
    public class ErrorLogger
    {
        public string? LastError { get; set; }

        public event EventHandler<Guid> ErrorLogged = (sender, guid) => {}; 
        
        public void Log(string? error)
        {
            if (String.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();
                
            LastError = error; 
            
            // Write the log to a storage
            // ...
            
            ErrorLogged.Invoke(this, Guid.NewGuid());
        }
    }
}