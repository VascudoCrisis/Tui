using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuiEmulator.Common.Models;

namespace TuiEmulator.Common.Services
{
    /// <summary>
    ///     Сервис мест
    /// </summary>
    public interface IDictService
    {
        /// <summary>
        ///     Получение списка городов отправления
        /// </summary>
        /// <param name="token">Токен отмены</param>
        /// <returns>Список городов</returns>
        Task<IEnumerable<City>> GetAllCitiesOfDeparture(CancellationToken token);

        /// <summary>
        ///     Получение спика городов отправления
        /// </summary>
        /// <param name="token">Токен отмены</param>
        /// <returns>Список городов</returns>
        Task<IEnumerable<City>> GetAllCitiesOdArrival(CancellationToken token);

        /// <summary>
        ///     Получение списка всех городов
        /// </summary>
        /// <returns>Список городов</returns>
        Task<IEnumerable<City>> GetAllCities();

        /// <summary>
        ///     Получение списка всех стран
        /// </summary>
        /// <returns>Список стран</returns>
        Task<IEnumerable<Country>> GetAllCountries();

        /// <summary>
        ///     Получение списка всех отелей
        /// </summary>
        /// <returns>Список отелей</returns>
        Task<IEnumerable<Hotel>> GetHotels();

        /// <summary>
        ///     Получение отеля по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отеля</param>
        /// <returns>Отель</returns>
        Task<Hotel> GetHotelById(int id);
    }
}