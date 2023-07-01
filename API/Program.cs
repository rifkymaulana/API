using System.Text;
using API.Contracts;
using API.Data;
using API.Repositories;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TokenHandler = API.Utilities.Handlers.TokenHandler;

namespace API;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        // Repositories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IEducationRepository, EducationRepository>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();

        // Services
        builder.Services.AddScoped<UniversityService>();
        builder.Services.AddScoped<RoomService>();
        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<EmployeeService>();
        builder.Services.AddScoped<BookingService>();
        builder.Services.AddScoped<AccountService>();
        builder.Services.AddScoped<AccountRoleService>();
        builder.Services.AddScoped<EducationService>();

        // Register Handler
        builder.Services.AddScoped<GenerateNikHandler>();
        builder.Services.AddScoped<ITokenHandler, TokenHandler>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Jwt Configuration
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // For development
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWTService:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTService:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTService:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

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
