using API.Contracts;
using API.Contracts.IRepositories;
using API.Data;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        /*builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IEducationRepository, EducationRepository>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();*/

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}