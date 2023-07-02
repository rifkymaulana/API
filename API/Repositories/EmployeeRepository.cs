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
    
    public bool IsDuplicateValue(string value)
    {
        return _context.Set<Employee>()
            .FirstOrDefault(e => e.Email.Contains(value) || e.PhoneNumber.Contains(value)) is null;
    }
    
    public string? GetLastEmpoyeeNik()
    {
        return _context.Set<Employee>().ToList().Select(e => e.Nik).LastOrDefault();
    }
    
    public Employee? GetByEmail(string email, bool isEmail = true)
    {
        return _context.Employees.FirstOrDefault(e => e.Email == email);
    }
    
    public Employee? GetByEmailAndPhoneNumber(string data)
    {
        return _context.Set<Employee>().FirstOrDefault(e => e.PhoneNumber == data || e.Email == data);
    }
}


