using CurveDentalManagement.API.Data;
using Microsoft.EntityFrameworkCore;
using CurveDentalManagement.API.Repositories.Interface;
using CurveDentalManagement.API.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add & Configure Swagger
builder.Services.AddSwaggerGen();

// Load .env file
DotNetEnv.Env.Load();

// Configure connection string from environment variable
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_CurveDentalApp");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");
}

// Add connection string to the application's configuration system
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string> { { "ConnectionStrings:CurveDentalAppConnectionString", connectionString } });

// inject DbContect Into Application
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CurveDentalAppConnectionString"));
});

// inject repository into Application
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<ITreatmentRepository, TreatmentRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Add & Setup CORS Policy
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
