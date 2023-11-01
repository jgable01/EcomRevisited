using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services.Interfaces;

namespace EcomRevisited.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;

        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }

        public virtual async Task<Country> GetCountryByNameAsync(string name)
        {
            return await _countryRepository.GetByConditionAsync(country => country.Name == name);
        }
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

    }
}
