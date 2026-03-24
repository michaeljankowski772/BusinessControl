using BusinessControlService;
using BusinessControlService.Models;
using BusinessControlService.Models.Enums;
using BusinessControlService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.AllowAnyOrigin()
            //.WithOrigins("http://localhost:8081")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// for the purpose of simplicity we wont force strong passwords for now
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<JwtService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Workers.Any())
    {
        context.Workers.AddRange(
            new Worker { FirstName = "John", LastName = "Brown", IsHired = true, HiringDateStart = new DateTime(2015, 1, 1), HiringDateEnd = new DateTime(2017, 2, 2) },
            new Worker { FirstName = "Chris", LastName = "Grease", IsHired = true, HiringDateStart = new DateTime(2015, 1, 1), HiringDateEnd = new DateTime(2017, 2, 2) }
        );

        context.SaveChanges();
    }

    if (!context.Customers.Any())
    {
        context.Customers.AddRange(
            new Customer { FirstName = "Cus", LastName = "Tomer", Address = "Road 1/2", City = "City1" },
            new Customer { FirstName = "Leom", LastName = "Essi", Address = "Roadski", City = "London" }
        );

        context.SaveChanges();
    }

    if (!context.Machines.Any())
    {
        context.Machines.AddRange(
            new Machine { AcquisitionDate = new DateTime(2021, 12, 12, 8, 1, 1), MachineName = "Tractor1", MachineType = MachineTypeEnum.Tractor}
        );

        context.SaveChanges();
    }

    if (!context.WorkshopJobs.Any() && context.Workers.Count() > 0)
    {
        context.WorkshopJobs.AddRange(
            new WorkshopJob { Worker = context.Workers.First(), DateStart = new DateTime(2025, 12, 12, 8, 1, 1), DateEnd = new DateTime(2025, 12, 12, 15, 44, 43), Description = "Cleaning floor" }
        );

        context.SaveChanges();
    }

    if (!context.FieldJobs.Any() && context.Workers.Count() > 0)
    {
        context.FieldJobs.AddRange(
            new FieldJob { Worker = context.Workers.First(), Customer = context.Customers.First(), FieldArea = 5.5F, Latitude = 51.5F, Longitude = 50.5F, Machine = context.Machines.First(), DateStart = new DateTime(2025, 12, 12, 8, 1, 1), DateEnd = new DateTime(2025, 12, 12, 15, 44, 43)}
        );

        context.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseCors("AllowReact");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
