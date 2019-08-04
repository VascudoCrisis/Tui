using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Services;

namespace TuiEmulator.Api.Controllers
{
    /// <summary>
    ///     Места
    /// </summary>
    [Route("locations")]
    public class LocationsController : Controller
    {
        private readonly IDictService _dictService;

        public LocationsController(IDictService dictService)
        {
            _dictService = dictService;
        }

        /// <summary>
        ///     Получение списка всех стран
        /// </summary>
        /// <returns>Список стран</returns>
        [HttpGet("countries")]
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _dictService.GetAllCountries();
        }

        /// <summary>
        ///     Получение списка всех городов прибытия
        /// </summary>
        /// <returns>Список городов</returns>
        [HttpGet("cities/arrival")]
        public async Task<IEnumerable<City>> GetCitiesOfArrival()
        {
            return await _dictService.GetAllCitiesOdArrival(CancellationToken.None);
        }

        /// <summary>
        ///     Список городов отправления
        /// </summary>
        /// <returns>Список городов</returns>
        [HttpGet("cities/departure")]
        public async Task<IEnumerable<City>> GetCitiesOfDeparture()
        {
            return await _dictService.GetAllCitiesOfDeparture(CancellationToken.None);
        }

        /// <summary>
        ///     Получение списка отелей
        /// </summary>
        /// <returns>Список отелей</returns>
        [HttpGet("hotels")]
        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            return await _dictService.GetHotels();
        }

        /// <summary>
        ///     Получение отеля по Id
        /// </summary>
        /// <param name="id">Идентификатор отеля</param>
        /// <returns>Отель</returns>
        [HttpGet("hotels/{id}")]
        public async Task<Hotel> GetHotelById(int id)
        {
            return await _dictService.GetHotelById(id);
        }
    }
}