﻿using API.Contracts;
using API.DTOs.Employees;
using API.Models;

namespace API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<GetEmployeeDto>? GetEmployee()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return null; // No employee  found
            }

            var toDto = employees.Select(employee =>
                                                new GetEmployeeDto
                                                {
                                                    Guid = employee.Guid,
                                                    Nik = employee.Nik,
                                                    BirthDate = employee.BirthDate,
                                                    Email = employee.Email,
                                                    FirstName = employee.FirstName,
                                                    LastName = employee.LastName,
                                                    Gender = employee.Gender,
                                                    HiringDate = employee.HiringDate,
                                                    PhoneNumber = employee.PhoneNumber
                                                }).ToList();

            return toDto; // employee found
        }

        public GetEmployeeDto? GetEmployee(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return null; // employee not found
            }

            var toDto = new GetEmployeeDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                BirthDate = employee.BirthDate,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber
            };

            return toDto; // employees found
        }

        public GetEmployeeDto? CreateEmployee(NewEmployeeDto newEmployeeDto)
        {
            var employee = new Employee
            {
                Guid = new Guid(),
                PhoneNumber = newEmployeeDto.PhoneNumber,
                FirstName = newEmployeeDto.FirstName,
                LastName = newEmployeeDto.LastName,
                Gender = newEmployeeDto.Gender,
                HiringDate = newEmployeeDto.HiringDate,
                Email = newEmployeeDto.Email,
                BirthDate = newEmployeeDto.BirthDate,
                Nik = newEmployeeDto.Nik,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdEmployee = _employeeRepository.Create(employee);
            if (createdEmployee is null)
            {
                return null; // employee not created
            }

            var toDto = new GetEmployeeDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                BirthDate = employee.BirthDate,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber
            };

            return toDto; // employee created
        }

        public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var isExist = _employeeRepository.IsExist(updateEmployeeDto.Guid);
            if (!isExist)
            {
                return -1; // employee not found
            }

            var getEmployee = _employeeRepository.GetByGuid(updateEmployeeDto.Guid);

            var employee = new Employee
            {
                Guid = updateEmployeeDto.Guid,
                PhoneNumber = updateEmployeeDto.PhoneNumber,
                FirstName = updateEmployeeDto.FirstName,
                LastName = updateEmployeeDto.LastName,
                Gender = updateEmployeeDto.Gender,
                HiringDate = updateEmployeeDto.HiringDate,
                Email = updateEmployeeDto.Email,
                BirthDate = updateEmployeeDto.BirthDate,
                Nik = updateEmployeeDto.Nik,
                ModifiedDate = DateTime.Now,
                CreatedDate = getEmployee!.CreatedDate
            };

            var isUpdate = _employeeRepository.Update(employee);
            if (!isUpdate)
            {
                return 0; // employee not updated
            }

            return 1;
        }

        public int DeleteEmployee(Guid guid)
        {
            var isExist = _employeeRepository.IsExist(guid);
            if (!isExist)
            {
                return -1; // employee not found
            }

            var employee = _employeeRepository.GetByGuid(guid);
            var isDelete = _employeeRepository.Delete(employee!);
            if (!isDelete)
            {
                return 0; // employee not deleted
            }

            return 1;
        }
    }
}
