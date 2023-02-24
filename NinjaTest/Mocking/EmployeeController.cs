using Microsoft.EntityFrameworkCore;

namespace NinjaTest.Mocking
{
    public class EmployeeController
    {
        private IEmployeeRepository _db;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _db = employeeRepository;
        }

        public ActionResult DeleteEmployee(int id)
        {
            _db.RemoveById(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public interface IEmployeeRepository
    {
        public Task RemoveById(int id);
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}