using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuiEmulator.Common;
using TuiEmulator.Common.Models;

namespace TuiEmulator.Api.Controllers
{
    [Route("locations")]
    public class LocationsController : Controller
    {
        private readonly IDictService _dictService;

        public LocationsController(IDictService dictService)
        {
            _dictService = dictService;
        }

        [HttpGet("countries")]
        public async Task<Country[]> GetAllCountries()
        {
            return (await _dictService.GetAllCountries()).ToArray();
        }

        [HttpGet("cities/arrival")]
        public async Task<City[]> GetAllCitiesOfArrival()
        {
            return (await _dictService.GetAllCitiesOdArrival(CancellationToken.None)).ToArray();
        }

        [HttpGet("cities/departure")]
        public async Task<City[]> GetAllCitiesOfDeparture()
        {
            return (await _dictService.GetAllCitiesOfDeparture(CancellationToken.None)).ToArray();
        }

        [HttpGet("hotels")]
        public async Task<Hotel[]> GetAllHotels()
        {
            return (await _dictService.GetHotels()).ToArray();
        }

        [HttpGet("hotels/{id}")]
        public async Task<Hotel> GetHotelById(int id)
        {
            return await _dictService.GetHotelById(id);
        }
    }
}