using EventHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationAuthentication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var migrationFlagPath = "/app/data/migrations_applied.flag";

    if (File.Exists(migrationFlagPath))
    {
        Console.WriteLine("Migrations already applied. Skipping.");
    }
    else
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        File.WriteAllText(migrationFlagPath, "Migrations applied on " + DateTime.Now);
        Console.WriteLine("Migrations applied and flag created.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAngular");

app.MapControllers();

app.Run();
