using System.Security.Claims;
using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Utilities;

namespace API.Services;

public class AccountService
{
    private readonly ApplicationDbContext _context;
    private readonly IAccountRepository _accountRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly ITokenHandler _tokenHandler;
    private readonly IEmailHandler _emailHandler;

    public AccountService(ApplicationDbContext context, IAccountRepository accountRepository,
        IUniversityRepository universityRepository, IEmployeeRepository employeeRepository,
        IEducationRepository educationRepository, IRoleRepository roleRepository,
        IAccountRoleRepository accountRoleRepository, ITokenHandler tokenHandler,
        IEmailHandler emailHandler)
    {
        _context = context;
        _accountRepository = accountRepository;
        _universityRepository = universityRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _tokenHandler = tokenHandler;
        _emailHandler = emailHandler;
    }

    public bool RegisterAccount(RegisterAccountDto registerAccountDto)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var university = _universityRepository.CreateWithDuplicateCheck(new CreateUniversityDto
            {
                Code = registerAccountDto.UniversityCode,
                Name = registerAccountDto.UniversityName
            });

            Employee employeeData = new CreateEmployeeDto
            {
                FirstName = registerAccountDto.FirstName,
                LastName = registerAccountDto.LastName,
                BirthDate = registerAccountDto.BirthDate,
                Gender = registerAccountDto.Gender,
                HiringDate = registerAccountDto.HiringDate,
                Email = registerAccountDto.Email,
                PhoneNumber = registerAccountDto.PhoneNumber
            };
            employeeData.Nik = GenerateNikHandler.Nik(_employeeRepository.GetLastEmpoyeeNik());

            var employee = _employeeRepository.Create(employeeData);

            _educationRepository.Create(new CreateEducationDto
            {
                Guid = employee.Guid,
                Major = registerAccountDto.Major,
                Degree = registerAccountDto.Degree,
                Gpa = registerAccountDto.Gpa,
                UniversityGuid = university!.Guid,
            });

            var account = _accountRepository.Create(new CreateAccountDto
            {
                EmployeeGuid = employee.Guid,
                Password = HashingHandler.HashPassword(registerAccountDto.Password)
            });

            var getRoleUser = _roleRepository.GetByName("User");
            _accountRoleRepository.Create(new CreateAccountRoleDto
            {
                AccountGuid = account.Guid,
                RoleGuid = getRoleUser!.Guid
            });

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public string LoginAccount(LoginAccountDto login)
    {
        var employee = _employeeRepository.GetByEmail(login.Email);
        if (employee is null)
            return "0";

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
            return "0";

        if (!HashingHandler.ValidatePassword(login.Password, account.Password))
            return "-1";

        try
        {
            var claims = new List<Claim>()
            {
                new Claim("NIK", employee.Nik),
                new Claim("FullName", $"{employee.FirstName} {employee.LastName}"),
                new Claim("EmailAddress", login.Email)
            };

            var getAccountRole = _accountRoleRepository.GetAccountRolesByAccountGuid(employee.Guid);
            var getRoleNameByAccountRole = from ar in getAccountRole
                join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                select r.Name;

            claims.AddRange(getRoleNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

            var getToken = _tokenHandler.GenerateToken(claims);
            return getToken;
        }
        catch
        {
            return "-2";
        }
    }

    public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var employee = _employeeRepository.GetByEmail(changePasswordDto.Email);
        if (employee is null)
            return 0; // Email not found
        
        var account = _accountRepository.GetByGuid(employee.Guid);

        Console.WriteLine(employee.Email);
        Console.WriteLine(account.Otp);
        Console.WriteLine(account.ExpiredTime);
        
        if (account is null) return 0; // Email not found
        if (account.IsUsed == false) return -1; // OTP is used
        if (account.Otp != changePasswordDto.Otp) return -2; // OTP is incorrect
        if (account.ExpiredTime < DateTime.Now) return -3; // OTP is expired
        
        account.Password = HashingHandler.HashPassword(changePasswordDto.NewPassword);
        var isUpdated = _accountRepository.Update(account);
        
        return isUpdated ? 1    // Success
            : -4;  // Database Error
    }
    
    public int ForgotPassword(ForgotPasswordDto forgotPassword)
    {
        var employee = _employeeRepository.GetByEmail(forgotPassword.Email);
        if (employee is null)
            return 0; // Email not found

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
            return -1;

        var otp = new Random().Next(111111, 999999);

        account.Otp = otp.ToString();
        account.ExpiredTime = DateTime.Now.AddMinutes(5);
        var isUpdated = _accountRepository.Update(account);

        if (!isUpdated)
            return -1;

        Console.WriteLine($"account.Otp {account.Otp}");
        
        _emailHandler.SendEmail(forgotPassword.Email, 
            "Forgot Password", 
            $"Your OTP is {otp}");

        return 1;
    }
    
    public IEnumerable<GetAccountDto>? GetAccount()
    {
        var entities = _accountRepository.GetAll();
        if (!entities.Any()) return null;

        var entitiesDtos = entities.Select(u => new GetAccountDto
        {
            EmployeeGuid = u.Guid,
            Password = u.Password,
            IsDeleted = u.IsDeleted
        }).ToList();

        return entitiesDtos;
    }

    public GetAccountDto? GetAccount(Guid guid)
    {
        var entity = _accountRepository.GetByGuid(guid);
        if (entity == null) return null;

        var entityDto = new GetAccountDto()
        {
            EmployeeGuid = entity.Guid,
            Password = entity.Password,
            IsDeleted = entity.IsDeleted
        };

        return entityDto;
    }

    public GetAccountDto? CreateAccount(CreateAccountDto createEntityDto)
    {
        var entity = new Account()
        {
            Guid = createEntityDto.EmployeeGuid,
            Password = createEntityDto.Password,
            IsDeleted = createEntityDto.IsDeleted,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEntity = _accountRepository.Create(entity);
        if (createdEntity is null) return null;

        var entityDto = new GetAccountDto()
        {
            EmployeeGuid = createdEntity.Guid,
            Password = createdEntity.Password,
            IsDeleted = createdEntity.IsDeleted
        };

        return entityDto;
    }

    public int UpdateAccount(UpdateAccountDto updateEntityDto)
    {
        var entity = _accountRepository.IsExist(updateEntityDto.EmployeeGuid);
        if (!entity) return -1;

        var entityToUpdate = _accountRepository.GetByGuid(updateEntityDto.EmployeeGuid);

        entityToUpdate!.Password = updateEntityDto.Password;
        entityToUpdate.IsDeleted = updateEntityDto.IsDeleted;
        entityToUpdate.ModifiedDate = DateTime.Now;

        var isUpdated = _accountRepository.Update(entityToUpdate);
        if (!isUpdated) return 0;

        return 1;
    }

    public int DeleteAccountRole(Guid guid)
    {
        var entity = _accountRepository.IsExist(guid);
        if (!entity) return -1;

        var entityToDelete = _accountRepository.GetByGuid(guid);
        var isDeleted = _accountRepository.Delete(entityToDelete!);
        if (!isDeleted) return 0;

        return 1;
    }
}
