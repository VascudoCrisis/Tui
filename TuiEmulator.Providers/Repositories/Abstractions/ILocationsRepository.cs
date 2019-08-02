using System.Collections.Generic;
using System.Threading.Tasks;
using TuiEmulator.Common.Models;

namespace TuiEmulator.Providers.Repositories.Abstractions
{
    internal interface ILocationsRepository
    {
        Task<IEnumerable<City>> GetAllCities();
        Task<IEnumerable<Country>> GetAllCountries();
        Task<IEnumerable<Hotel>> GetAllHotels();
        Task<Hotel> GetHotelById(int id);
    }
}