using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TuiEmulator.Common;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Models.Requests;
using TuiEmulator.Common.Options;
using TuiEmulator.Providers.Extensions;
using TuiEmulator.Providers.Repositories.Static;

namespace TuiEmulator.Providers.Providers
{
    public class ProviderAggregator : IToursProviderService
    {
        private readonly ProviderAggregatorOptions _options;
        private readonly IEnumerable<IToursProviderService> _providers;

        public ProviderAggregator(IEnumerable<IToursProviderService> providers,
            IOptions<ProviderAggregatorOptions> options)
        {
            _providers = providers;
            _options = options.Value;
        }

        public async Task<IEnumerable<City>> GetAllCitiesOfDeparture(CancellationToken token)
        {
            var cts = new CancellationTokenSource();

            var tasks = _providers.Select(provider => provider.GetAllCitiesOfDeparture(cts.Token)).ToArray();

            cts.CancelAfter(_options.GetCitiesRequestTimeout);

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (TaskCanceledException)
            {
                foreach (var task in tasks.Where(t => t.IsCanceled))
                    Debug.WriteLine($"Task {task.Id} was cancelled by timeout");
            }

            return tasks.Where(t => t.IsCompleted).SelectMany(t => t.Result).Uniq(city => city.Id);
        }

        public async Task<IEnumerable<City>> GetAllCitiesOdArrival(CancellationToken token)
        {
            var cts = new CancellationTokenSource();

            var tasks = _providers.Select(provider => provider.GetAllCitiesOdArrival(cts.Token)).ToArray();

            cts.CancelAfter(_options.GetCitiesRequestTimeout);

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (TaskCanceledException)
            {
                foreach (var task in tasks.Where(t => t.IsCanceled))
                    Debug.WriteLine($"Task {task.Id} was cancelled by timeout");
            }

            return tasks.Where(t => t.IsCompleted).SelectMany(t => t.Result).Uniq(city => city.Id);
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await Task.FromResult(StaticRepository.Cities);
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await Task.FromResult(StaticRepository.Countries);
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await Task.FromResult(StaticRepository.Hotels);
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            return await Task.FromResult(StaticRepository.Hotels.Single(hotel => hotel.Id == id));
        }

        public async Task<IEnumerable<Tour>> Search(SearchRequest request, CancellationToken token)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(_options.SearchRequestTimeout);

            var tasks = _providers.Select(s => s.Search(request, cts.Token)).ToArray();
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (TaskCanceledException)
            {
                foreach (var task in tasks.Where(t => t.IsCanceled))
                    Debug.WriteLine($"Task {task.Id} was cancelled by timeout");
            }

            return tasks.Where(t => t.IsCompleted).SelectMany(task => task.Result);
        }
    }
}