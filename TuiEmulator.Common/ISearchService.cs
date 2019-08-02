using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Models.Requests;

namespace TuiEmulator.Common
{
    public interface ISearchService
    {
        Task<IEnumerable<Tour>> Search(SearchRequest request, CancellationToken token);
    }
}