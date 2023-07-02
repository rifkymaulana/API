using System.ComponentModel.DataAnnotations;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class CreateEmployeeDto
{
    [Required]
    public string Nik { get; set; }
    [Required]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    [Range(0,1, ErrorMessage = "0 = Female, 1 = Male")]
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    
    public static implicit operator Employee(CreateEmployeeDto newEmployeeDto)
    {
        return new() {
            Guid = new Guid(),
            FirstName = newEmployeeDto.FirstName,
            LastName = newEmployeeDto.LastName,
            BirthDate = newEmployeeDto.BirthDate,
            Gender = newEmployeeDto.Gender,
            HiringDate = newEmployeeDto.HiringDate,
            Email = newEmployeeDto.Email,
            PhoneNumber = newEmployeeDto.PhoneNumber,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
    
    public static explicit operator CreateEmployeeDto(Employee employee)
    {
        return new() {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        };
    }
}
