using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuiEmulator.Common.Models;

namespace TuiEmulator.Common
{
    public interface IDictService
    {
        Task<IEnumerable<City>> GetAllCitiesOfDeparture(CancellationToken token);
        Task<IEnumerable<City>> GetAllCitiesOdArrival(CancellationToken token);
        Task<IEnumerable<City>> GetAllCities();
        Task<IEnumerable<Country>> GetAllCountries();
        Task<IEnumerable<Hotel>> GetHotels();
        Task<Hotel> GetHotelById(int id);
    }
}