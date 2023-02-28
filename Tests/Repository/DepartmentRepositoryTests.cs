using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Persistence;

namespace OfficeStaff.Tests.Repository
{
    public class DepartmentRepositoryTests
    {
        private readonly IBaseRepository _iBaseRepository;
        public DepartmentRepositoryTests()
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

            if (await databaseContext.Departments.CountAsync() <= 0)
                databaseContext.Departments.AddRange(
                     new Department
                     {
                         Name = "Sales Department",
                         Location = new Location
                         {
                             Name = "Moscow",
                             Country = new Country { Name = "Russia" },
                         },
                     },
                    new Department
                    {
                        Name = "Sales Department",
                        Location = new Location
                        {
                            Name = "Saint-Petersburg",
                            Country = new Country { Name = "Russia" },
                        },
                    });


            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Fact]
        public async void DepartmentRepository_GetDepartment_ReturnDepartment()
        {
            // Arrange
            var departmentId = 1;
            var dbContext = await GetDatabaseContext();
            var departmentRepository = new DepartmentRepository(dbContext, _iBaseRepository);

            // Act
            var department = departmentRepository.GetDepartment(departmentId);

            // Assert 
            department.Should().NotBeNull();
            department.Should().BeOfType<Department>();
        }

        [Fact]
        public async void DepartmentRepository_DepartmentExists_ReturnTrue()
        {
            // Arrange
            var departmentId = 1;
            var dbContext = await GetDatabaseContext();
            var departmentRepository = new DepartmentRepository(dbContext, _iBaseRepository);

            // Act
            var department = departmentRepository.DepartmentExists(departmentId);

            // Assert 
            department.Should().BeTrue();
        }

        [Fact]
        public async void DepartmentRepository_GetDepartments_ReturnDepartments()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var departmentRepository = new DepartmentRepository(dbContext, _iBaseRepository);

            // Act
            var departments = departmentRepository.GetDepartments();

            // Assert 
            departments.Should().NotBeNull();
            departments.Should().HaveCount(c => c <= 2).And.OnlyHaveUniqueItems();
        }
    }
}