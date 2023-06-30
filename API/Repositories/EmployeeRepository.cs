﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }
    
    public IEnumerable<Employee> GetByName(string name)
    {
        return _context.Employees.Where(u => u.FirstName == name || u.LastName == name).ToList();
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


