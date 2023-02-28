using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Persistence;


namespace OfficeStaff.Tests.Repository
{
    public class LocationRepositoryTests
    {
        private readonly IBaseRepository _iBaseRepository;
        public LocationRepositoryTests()
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

            if (await databaseContext.Locations.CountAsync() <= 0)
                databaseContext.Locations.AddRange(
                    new Location
                {
                    Name = "Moscow",
                    Country = new Country { Name = "Russia" },
                }, 
                    new Location { 
                        Name = "London",
                        Country = new Country { Name = "UK" }
                    });

            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Fact]
        public async void LocationRepository_GetLocation_ReturnLocation()
        {
            // Arrange
            var locationId = 1;
            var dbContext = await GetDatabaseContext();
            var locationRepository = new LocationRepository(dbContext, _iBaseRepository);

            // Act
            var location = locationRepository.GetLocation(locationId);

            // Assert 
            location.Should().NotBeNull();
            location.Should().BeOfType<Location>();
        }

        [Fact]
        public async void LocationRepository_LocationExists_ReturnTrue()
        {
            // Arrange
            var locationId = 1;
            var dbContext = await GetDatabaseContext();
            var locationRepository = new LocationRepository(dbContext, _iBaseRepository);

            // Act
            var location = locationRepository.LocationExists(locationId);

            // Assert 
            location.Should().BeTrue();
        }

        [Fact]
        public async void LocationRepository_GetLocations_ReturnLocations()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var locationRepository = new LocationRepository(dbContext, _iBaseRepository);

            // Act
            var locations = locationRepository.GetLocations();

            // Assert 
            locations.Should().NotBeNull();
            locations.Should().HaveCount(c => c <= 2).And.OnlyHaveUniqueItems();
        }
    }
}