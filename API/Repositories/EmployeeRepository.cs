using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public IEnumerable<Employee> GetByName(string name)
    {
        return _context.Employees.Where(u => u.FirstName == name || u.LastName == name).ToList();
    }
    
    public Employee? GetByEmailAndPhoneNumber(string data)
    {
        return _context.Set<Employee>().FirstOrDefault(e => e.PhoneNumber == data || e.Email == data);
    }
}
