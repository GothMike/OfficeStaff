﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using OfficeStaff.Data.Models;
using OfficeStaff.Data.Repository;
using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Persistence;
using System.Collections;

namespace OfficeStaff.Tests.Repository
{
    public class CountryRepositoryTests
    {
        private readonly IBaseRepository _iBaseRepository;
        public CountryRepositoryTests()
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

            if (await databaseContext.Countries.CountAsync() <= 0)
                databaseContext.Countries.AddRange(new Country { Name = "Russia" }, new Country { Name = "USA" });

            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Fact]
        public async void CountryRepository_GetCountry_ReturnCountry()
        {
            // Arrange
            var countryId = 1;
            var dbContext = await GetDatabaseContext();
            var countryRepository = new CountryRepository(dbContext, _iBaseRepository);

            // Act
            var result = countryRepository.GetCountry(countryId);

            // Assert 
            result.Should().NotBeNull();
            result.Should().BeOfType<Country>();
        }

        [Fact]
         public async void CountryRepository_GetExists_ReturnTrue()
        {
            // Arrange
            var countryId = 1;
            var dbContext = await GetDatabaseContext();
            var countryRepository = new CountryRepository(dbContext, _iBaseRepository);

            // Act
            var result = countryRepository.CountryExists(countryId);

            // Assert 
            result.Should().BeTrue();
        }

        [Fact]
        public async void CountryRepository_GetCountries_ReturnCountries()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var countryRepository = new CountryRepository(dbContext, _iBaseRepository);

            // Act
            var result = countryRepository.GetCountries();

            // Assert 
            result.Should().NotBeNull();
            result.Should().HaveCount(c => c <= 2).And.OnlyHaveUniqueItems();
        }

        [Fact]
        public async void CountryRepository_CreateCountry_ReturnCountry()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var countryRepository = new CountryRepository(dbContext, _iBaseRepository);

            // Act
            var country = new Country() { Name = "United Kingdom" };

            // Assert 
            country.Name.Should().NotBeNull();
        }

        [Fact]
        public async void CountryRepository_DeleteCountry_ReturnCountry()
        {
            // Arrange
            int countryId = 1;
            var dbContext = await GetDatabaseContext();
            var countryRepository = new CountryRepository(dbContext, _iBaseRepository);

            // Act
            var country = countryRepository.GetCountry(countryId);

            // Assert 
            country.Name.Should().NotBeNull();
        }

        [Fact]
        public async void CountryRepository_UpdateCountry_ReturnCountry()
        {
            // Arrange
            int countryId = 1;
            var dbContext = await GetDatabaseContext();
            var countryRepository = new CountryRepository(dbContext, _iBaseRepository);

            // Act
            var country = countryRepository.GetCountry(countryId);

            // Assert 
            country.Name.Should().NotBeNull();
        }
    }
}