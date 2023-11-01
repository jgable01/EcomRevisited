using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services;

namespace EcomRevisited.Tests
{
    [TestClass]
    public class CountryServiceTests
    {
        private Mock<IRepository<Country>> _countryRepoMock = new Mock<IRepository<Country>>();

        [TestMethod]
        public async Task GetAllCountriesAsync_ReturnsAllCountries()
        {
            // Arrange
            var expectedCountries = new List<Country>
        {
            new Country { Id = Guid.NewGuid() },
            new Country { Id = Guid.NewGuid() }
        };
            _countryRepoMock.Setup(p => p.GetAllAsync()).ReturnsAsync(expectedCountries);

            var service = new CountryService(_countryRepoMock.Object);

            // Act
            var actualCountries = await service.GetAllCountriesAsync();

            // Assert
            Assert.AreEqual(expectedCountries.Count, actualCountries.Count());
        }

        [TestMethod]
        public async Task GetCountryByIdAsync_CountryExists_ReturnsCountry()
        {
            // Arrange
            var countryId = Guid.NewGuid();
            var expectedCountry = new Country { Id = countryId };
            _countryRepoMock.Setup(p => p.GetByIdAsync(countryId)).ReturnsAsync(expectedCountry);

            var service = new CountryService(_countryRepoMock.Object);

            // Act
            var actualCountry = await service.GetCountryByIdAsync(countryId);

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }

        [TestMethod]
        public async Task GetCountryByIdAsync_CountryDoesNotExist_ReturnsNull()
        {
            // Arrange
            var countryId = Guid.NewGuid();
            _countryRepoMock.Setup(p => p.GetByIdAsync(countryId)).ReturnsAsync((Country)null);

            var service = new CountryService(_countryRepoMock.Object);

            // Act
            var actualCountry = await service.GetCountryByIdAsync(countryId);

            // Assert
            Assert.IsNull(actualCountry);
        }

        [TestMethod]
        public async Task GetCountryByNameAsync_CountryExists_ReturnsCountry()
        {
            // Arrange
            var countryName = "Canada";
            var expectedCountry = new Country { Name = countryName };
            _countryRepoMock.Setup(p => p.GetByConditionAsync(country => country.Name == countryName)).ReturnsAsync(expectedCountry);

            var service = new CountryService(_countryRepoMock.Object);

            // Act
            var actualCountry = await service.GetCountryByNameAsync(countryName);

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }

        [TestMethod]
        public async Task GetCountryByNameAsync_CountryDoesNotExist_ReturnsNull()
        {
            // Arrange
            var countryName = "Unknown";
            _countryRepoMock.Setup(p => p.GetByConditionAsync(country => country.Name == countryName)).ReturnsAsync((Country)null);

            var service = new CountryService(_countryRepoMock.Object);

            // Act
            var actualCountry = await service.GetCountryByNameAsync(countryName);

            // Assert
            Assert.IsNull(actualCountry);
        }
    }
}
