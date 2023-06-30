using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasIndex(employee => new
            {
                employee.Nik,
                employee.Email,
                employee.PhoneNumber
            }).IsUnique();

        modelBuilder.Entity<University>()
            .HasMany(university => university.Educations)
            .WithOne(education => education.University)
            .HasForeignKey(education => education.UniversityGuid);

        modelBuilder.Entity<Education>()
            .HasOne(education => education.Employee)
            .WithOne(employee => employee.Education)
            .HasForeignKey<Education>(education => education.Guid);

        modelBuilder.Entity<Employee>()
            .HasMany(employee => employee.Bookings)
            .WithOne(booking => booking.Employee)
            .HasForeignKey(booking => booking.EmployeeGuid);

        modelBuilder.Entity<Booking>()
            .HasOne(booking => booking.Room)
            .WithMany(room => room.Bookings)
            .HasForeignKey(room => room.RoomGuid);

        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Account)
            .WithOne(account => account.Employee)
            .HasForeignKey<Account>(account => account.Guid);

        modelBuilder.Entity<Account>()
            .HasMany(account => account.AccountRoles)
            .WithOne(accountRole => accountRole.Account)
            .HasForeignKey(accountRole => accountRole.AccountGuid);

        modelBuilder.Entity<AccountRole>()
            .HasOne(account_role => account_role.Role)
            .WithMany(role => role.AccountRoles)
            .HasForeignKey(role => role.RoleGuid);
    }
}
