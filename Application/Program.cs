using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Interfaces;
using OfficeStaff.Data.Repository;
using OfficeStaff.Persistence;

var modelBuilder = WebApplication.CreateBuilder(args);


modelBuilder.Services.AddControllers();
modelBuilder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

modelBuilder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
modelBuilder.Services.AddScoped<ICountryRepository, CountryRepository>();
modelBuilder.Services.AddScoped<ILocationRepository, LocationRepository>();
modelBuilder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
modelBuilder.Services.AddScoped<IPositionRepository, PositionRepository>();

modelBuilder.Services.AddEndpointsApiExplorer();
modelBuilder.Services.AddSwaggerGen();

modelBuilder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(modelBuilder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = modelBuilder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
