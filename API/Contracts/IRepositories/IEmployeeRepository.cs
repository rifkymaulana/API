using API.Models;

namespace API.Contracts;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    public IEnumerable<Employee>? GetByName(string name);
    bool IsDuplicateValue(string value);
    string? GetLastEmpoyeeNik();
    public Employee? GetByEmail(string email, bool isEmail = true);
    public Employee? GetByEmailAndPhoneNumber(string data);
}
