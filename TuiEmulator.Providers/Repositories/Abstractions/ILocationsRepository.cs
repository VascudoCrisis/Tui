using System.Collections.Generic;
using System.Threading.Tasks;
using TuiEmulator.Common.Models;

namespace TuiEmulator.Providers.Repositories.Abstractions
{
    /// <summary>
    ///     Сервис мест
    /// </summary>
    internal interface ILocationsRepository
    {
        /// <summary>
        ///     Получение списка всех городов
        /// </summary>
        /// <returns>Список стран</returns>
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
        Task<IEnumerable<Hotel>> GetAllHotels();

        /// <summary>
        ///     Получение отеля по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отеля</param>
        /// <returns>Отель</returns>
        Task<Hotel> GetHotelById(int id);
    }
}