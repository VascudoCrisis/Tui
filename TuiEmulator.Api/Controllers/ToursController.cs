using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Requests;
using TuiEmulator.Common.Services;

namespace TuiEmulator.Api.Controllers
{
    /// <summary>
    ///     Туры
    /// </summary>
    [Route("tours")]
    public class ToursController : Controller
    {
        private readonly ISearchService _searchService;

        public ToursController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        ///     Поиск туров
        /// </summary>
        /// <param name="request">Поисковый запрос</param>
        /// <returns>Список найденых туров</returns>
        [HttpPost]
        public async Task<IEnumerable<Tour>> Search([FromBody] SearchRequest request)
        {
            return await _searchService.Search(request, CancellationToken.None);
        }
    }
}