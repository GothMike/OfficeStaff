using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Repository;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Services;
using OfficeStaff.Data.Services.Interfaces;
using OfficeStaff.Persistence;

var modelBuilder = WebApplication.CreateBuilder(args);


modelBuilder.Services.AddControllers();
modelBuilder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Репозитории
modelBuilder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
modelBuilder.Services.AddScoped<ICountryRepository, CountryRepository>();
modelBuilder.Services.AddScoped<ILocationRepository, LocationRepository>();
modelBuilder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
modelBuilder.Services.AddScoped<IPositionRepository, PositionRepository>();
modelBuilder.Services.AddScoped<IBaseRepository, BaseRepository>();

// Сервисы
modelBuilder.Services.AddScoped<IEmployeeService, EmployeeService>();
modelBuilder.Services.AddScoped<ICountryService, CountryService>();
modelBuilder.Services.AddScoped<IPositionService, PositionService>();
modelBuilder.Services.AddScoped<ILocationService, LocationService>();
modelBuilder.Services.AddScoped<IDepartmentService, DepartmentService>();

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
