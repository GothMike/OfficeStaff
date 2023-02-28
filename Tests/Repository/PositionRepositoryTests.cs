using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Data.Repository;
using OfficeStaff.Persistence;
using FluentAssertions;

namespace OfficeStaff.Tests.Repository
{
    public class PositionRepositoryTests
    {
        private readonly IBaseRepository _iBaseRepository;
        public PositionRepositoryTests()
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

            if (await databaseContext.Positions.CountAsync() <= 0)
                databaseContext.Positions.AddRange
                   (
                   new Position { Name = "Sales co-worker", IsAManagerPosition = false },
                   new Position { Name = "HR co-worker", IsAManagerPosition = false },
                   new Position { Name = "HR manager", IsAManagerPosition = true }
                   );

            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Fact]
        public async void PositionRepository_GetPosition_ReturnPosition()
        {
            // Arrange
            var positionId = 1;
            var dbContext = await GetDatabaseContext();
            var positionRepository = new PositionRepository(dbContext, _iBaseRepository);

            // Act
            var position = positionRepository.GetPosition(positionId);

            // Assert 
            position.Should().NotBeNull();
            position.Should().BeOfType<Position>();
        }

        [Fact]
        public async void PositionRepository_PositionExists_ReturnTrue()
        {
            // Arrange
            var positionId = 1;
            var dbContext = await GetDatabaseContext();
            var positionRepository = new PositionRepository(dbContext, _iBaseRepository);

            // Act
            var position = positionRepository.PositionExists(positionId);

            // Assert 
            position.Should().BeTrue();
        }

        [Fact]
        public async void PositionRepository_GetPositions_ReturnPositions()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var positionRepository = new PositionRepository(dbContext, _iBaseRepository);

            // Act
            var employees = positionRepository.GetPositions();

            // Assert 
            employees.Should().NotBeNull();
            employees.Should().HaveCount(c => c <= 3).And.OnlyHaveUniqueItems();
        }
    }
}