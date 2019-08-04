namespace TuiEmulator.Common.Options
{
    /// <summary>
    ///     Настройки поставщика туров
    /// </summary>
    public class ProviderOptions
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Количество туров для генерации
        /// </summary>
        public long ToursCount { get; set; }
    }
}