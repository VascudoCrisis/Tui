using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuiEmulator.Common;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Models.Requests;

namespace TuiEmulator.Api.Controllers
{
    [Route("tours")]
    public class ToursController : Controller
    {
        private readonly ISearchService _searchService;

        public ToursController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IEnumerable<Tour>> Search([FromBody] SearchRequest request)
        {
            return await _searchService.Search(request, CancellationToken.None);
        }
    }
}