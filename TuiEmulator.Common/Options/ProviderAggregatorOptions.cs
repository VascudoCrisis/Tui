namespace TuiEmulator.Common.Options
{
    /// <summary>
    ///     Настройки аггрегатора поставщиков туров
    /// </summary>
    public class ProviderAggregatorOptions
    {
        /// <summary>
        ///     Таймаут запроса поиска туров
        /// </summary>
        public int SearchRequestTimeout { get; set; }

        /// <summary>
        ///     Таймаут запроса получения списка городов
        /// </summary>
        public int GetCitiesRequestTimeout { get; set; }

        /// <summary>
        ///     Идентификатор приоритетного провайдера
        /// </summary>
        public int PriorityProviderId { get; set; }

        /// <summary>
        ///     Максимальный процент на который может отличатся цена приоритетного провадера при выборе тура из списка доступных
        /// </summary>
        public decimal MaxPercentPrice { get; set; }
    }
}