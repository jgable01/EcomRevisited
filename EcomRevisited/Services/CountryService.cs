namespace EcomRevisited.Services
{
    using System.Threading.Tasks;
    using global::EcomRevisited.Data;
    using global::EcomRevisited.Models;

    namespace EcomRevisited.Services
    {
        public class CountryService
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

            public async Task<Country> GetCountryByNameAsync(string name)
            {
                return await _countryRepository.GetByConditionAsync(country => country.Name == name);
            }
        }
    }

}
