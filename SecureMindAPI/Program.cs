using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Contract;
using SecureMindAPI.Data;
using SecureMindAPI.Extensions;
using SecureMindAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddScoped<ICrimeIncident,CrimeIncidentRepository>();
builder.Services.AddScoped<ICounsellorsRepository, CounsellorRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddHttpClient(); // Register IHttpClientFactory
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Angular application URL
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecureMind"));
});
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);
app.UseCors("AllowMyOrigin");

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
