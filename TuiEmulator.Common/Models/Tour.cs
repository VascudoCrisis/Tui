using System;

namespace TuiEmulator.Common.Models
{
    /// <summary>
    ///     Тур
    /// </summary>
    public class Tour
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Поставщик тура
        /// </summary>
        public TourProvider Provider { get; set; }

        /// <summary>
        ///     Отель
        /// </summary>
        public Hotel Hotel { get; set; }

        /// <summary>
        ///     Тип апартаментов
        /// </summary>
        public ApartmentsType ApartmentsType { get; set; }

        /// <summary>
        ///     Город отправления
        /// </summary>
        public City CityOfDeparture { get; set; }

        /// <summary>
        ///     Город тура
        /// </summary>
        public City CityOfArrival { get; set; }

        /// <summary>
        ///     Дата отправления
        /// </summary>
        public DateTimeOffset DateOfDeparture { get; set; }

        /// <summary>
        ///     Дата прибытия
        /// </summary>
        public DateTimeOffset DateOfArrival { get; set; }

        /// <summary>
        ///     Дата заезда
        /// </summary>
        public DateTimeOffset CheckInDate { get; set; }

        /// <summary>
        ///     Количество ночей
        /// </summary>
        public int Nights { get; set; }

        /// <summary>
        ///     Цена за одного гостя
        /// </summary>
        public decimal PricePerGuest { get; set; }

        /// <summary>
        ///     Максимальное количество гостей
        /// </summary>
        public int MaxGuestCount { get; set; }
    }
}