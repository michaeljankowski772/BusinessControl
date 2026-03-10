using Microsoft.EntityFrameworkCore;
using BusinessControlService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Workers.Any())
    {
        context.Workers.AddRange(
            new Worker { FirstName = "John", LastName = "Brown", IsHired = true, HiringDateStart = new DateTime(2015, 1, 1), HiringDateEnd = new DateTime(2017,2,2)},
            new Worker { FirstName = "Chris", LastName = "Grease", IsHired = true, HiringDateStart = new DateTime(2015, 1, 1), HiringDateEnd = new DateTime(2017, 2, 2) }
        );

        context.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
