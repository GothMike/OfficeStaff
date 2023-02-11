using Microsoft.EntityFrameworkCore;

var modelBuilder = WebApplication.CreateBuilder(args);


modelBuilder.Services.AddControllers();
modelBuilder.Services.AddEndpointsApiExplorer();
modelBuilder.Services.AddSwaggerGen();
modelBuilder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

modelBuilder.Services.AddDbContext<OfficeStaff.Persistence.AppContext>(options =>
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
