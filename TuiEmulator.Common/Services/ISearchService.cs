using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Requests;

namespace TuiEmulator.Common.Services
{
    /// <summary>
    ///     Сервис поиска
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        ///     Поиск туров
        /// </summary>
        /// <param name="request">Запрос поиска туров</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Список найденых туров</returns>
        Task<IEnumerable<Tour>> Search(SearchRequest request, CancellationToken token);
    }
}