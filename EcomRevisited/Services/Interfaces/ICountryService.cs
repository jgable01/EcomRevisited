using EcomRevisited.Models;

namespace EcomRevisited.Services.Interfaces
{
    public interface ICountryService
    {
        Task<Country> GetCountryByIdAsync(Guid id);
        Task<Country> GetCountryByNameAsync(string name);
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}
