using System;
using TuiEmulator.Common.Enums;

namespace TuiEmulator.Common.Requests
{
    /// <summary>
    ///     Запрос поиска туров
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        ///     Поле для сортировки
        /// </summary>
        public OrderBy OrderBy { get; set; } = OrderBy.Date;

        /// <summary>
        ///     Направление сортировки
        /// </summary>
        public OrderDirection OrderDirection { get; set; } = OrderDirection.Asc;

        /// <summary>
        ///     Поля поиска
        /// </summary>
        public SearchFields Fields { get; set; }

        /// <summary>
        ///     Количество элементов в выборке
        /// </summary>
        public int Take { get; set; } = 1000;

        /// <summary>
        ///     Поля поиска
        /// </summary>
        public class SearchFields
        {
            /// <summary>
            ///     Идентификатор города отправления
            /// </summary>
            public int? CityOfDepartureId { get; set; }

            /// <summary>
            ///     Идентификатор города тура
            /// </summary>
            public int? CityOfTourId { get; set; }

            /// <summary>
            ///     Дата старта тура
            /// </summary>
            public DateTimeOffset? TourStartDate { get; set; }

            /// <summary>
            ///     Минимальное количество ночей
            /// </summary>
            public int? NightsFrom { get; set; }

            /// <summary>
            ///     Максимальное количество ночей
            /// </summary>
            public int? NightsTo { get; set; }

            /// <summary>
            ///     Количество гостей
            /// </summary>
            public int? GuestsCount { get; set; }
        }
    }
}