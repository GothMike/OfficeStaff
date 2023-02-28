using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Repository;
using OfficeStaff.Persistence;
using FluentAssertions;

namespace OfficeStaff.Tests.Repository
{
    public class EmployeeRepositoryTests
    {
        private readonly IBaseRepository _iBaseRepository;
        public EmployeeRepositoryTests()
        {
            _iBaseRepository = A.Fake<IBaseRepository>();
        }

        public async Task<ApplicationContext> GetDatabaseContext()
        {

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                           .Options;
            var databaseContext = new ApplicationContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Employees.CountAsync() <= 0)
                databaseContext.Employees.AddRange(
                     new Employee { FirstName = "Mike", LastName = "Warren", 
                         
                         Department = new Department
                     {
                         Name = "Sales Department",
                         Location = new Location
                         {
                             Name = "Moscow",
                             Country = new Country { Name = "Russia" },
                         },
                     },
                         Position = new Position { Name = "Sales co-worker"}
                    });


            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Fact]
        public async void EmployeeRepository_GetEmployee_ReturnEmployee()
        {
            // Arrange
            var employeeId = 1;
            var dbContext = await GetDatabaseContext();
            var employeeRepository = new EmployeeRepository(dbContext, _iBaseRepository);

            // Act
            var employee = employeeRepository.GetEmployee(employeeId);

            // Assert 
            employee.Should().NotBeNull();
            employee.Should().BeOfType<Employee>();
        }

        [Fact]
        public async void EmployeeRepository_EmployeeExists_ReturnTrue()
        {
            // Arrange
            var employeeId = 1;
            var dbContext = await GetDatabaseContext();
            var employeeRepository = new EmployeeRepository(dbContext, _iBaseRepository);

            // Act
            var employee = employeeRepository.EmployeeExists(employeeId);

            // Assert 
            employee.Should().BeTrue();
        }

        [Fact]
        public async void EmployeeRepository_GetEmployees_ReturnEmployees()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var employeeRepository = new EmployeeRepository(dbContext, _iBaseRepository);

            // Act
            var employees = employeeRepository.GetEmployees();

            // Assert 
            employees.Should().NotBeNull();
            employees.Should().HaveCount(c => c <= 1).And.OnlyHaveUniqueItems();
        }
    }
}