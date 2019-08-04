namespace TuiEmulator.Common.Models
{
    /// <summary>
    ///     Отель
    /// </summary>
    public class Hotel
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Город
        /// </summary>
        public City City { get; set; }

        /// <summary>
        ///     Дата постройки
        /// </summary>
        public int YearOfConstruction { get; set; }
    }
}