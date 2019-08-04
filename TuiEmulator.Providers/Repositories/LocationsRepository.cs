using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuiEmulator.Common.Models;
using TuiEmulator.Providers.Repositories.Abstractions;
using TuiEmulator.Providers.Repositories.Static;

namespace TuiEmulator.Providers.Repositories
{
    /// <summary>
    ///     Репозиторий мест
    /// </summary>
    internal class LocationsRepository : ILocationsRepository
    {
        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await Task.FromResult(StaticRepository.Cities);
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await Task.FromResult(StaticRepository.Countries);
        }

        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            return await Task.FromResult(StaticRepository.Hotels);
        }

        public Task<Hotel> GetHotelById(int id)
        {
            return Task.FromResult(StaticRepository.Hotels.Single(hotel => hotel.Id == id));
        }
    }
}