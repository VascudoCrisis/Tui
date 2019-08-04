using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Options;
using TuiEmulator.Common.Requests;
using TuiEmulator.Common.Services;
using TuiEmulator.Providers.Extensions;
using TuiEmulator.Providers.Repositories.Static;

namespace TuiEmulator.Providers.Providers
{
    /// <summary>
    ///     Аггрегатор постащиков туров
    /// </summary>
    internal class ProviderAggregator : IToursProviderService
    {
        private readonly ProviderAggregatorOptions _options;
        private readonly IEnumerable<IToursProviderService> _providers;

        public ProviderAggregator(IEnumerable<IToursProviderService> providers,
            IOptions<ProviderAggregatorOptions> options)
        {
            _providers = providers;
            _options = options.Value;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<City>> GetAllCitiesOfDeparture(CancellationToken token)
        {
            using (var cts = new CancellationTokenSource())
            {
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

                return tasks.Where(t => t.IsCompleted)
                    .SelectMany(t => t.Result)
                    .Uniq(city => city.Id);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<City>> GetAllCitiesOdArrival(CancellationToken token)
        {
            using (var cts = new CancellationTokenSource())
            {
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

                return tasks.Where(t => t.IsCompleted)
                    .SelectMany(t => t.Result)
                    .Uniq(city => city.Id);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await Task.FromResult(StaticRepository.Cities);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await Task.FromResult(StaticRepository.Countries);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await Task.FromResult(StaticRepository.Hotels);
        }

        /// <inheritdoc />
        public async Task<Hotel> GetHotelById(int id)
        {
            return await Task.FromResult(StaticRepository.Hotels.Single(hotel => hotel.Id == id));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Tour>> Search(SearchRequest request, CancellationToken token)
        {
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(_options.SearchRequestTimeout);

                var tasks = _providers.Select(s => s.Search(request, cts.Token)).ToArray();

                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (TaskCanceledException e)
                {
                    foreach (var task in tasks.Where(t => t.IsCanceled))
                        Debug.WriteLine($"Task {task.Id} was cancelled by timeout");
                }

                return tasks.Where(t => t.IsCompleted).SelectMany(task => task.Result)
                    .GroupBy(tour =>
                        new
                        {
                            HotelId = tour.Hotel.Id,
                            CityOfDeparture = tour.CityOfDeparture.Id,
                            DateOfDeparture = tour.DateOfDeparture.ToString("yyMMdd"),
                            tour.Nights,
                            tour.ApartmentsType
                        })
                    .Select(group => BestMatch(group.ToArray()));
            }
        }

        /// <summary>
        ///     Получение лучшего предложения тура среди провайдеров
        /// </summary>
        /// <param name="tours">Лучшее предложение среди списка туров</param>
        /// <returns></returns>
        private Tour BestMatch(Tour[] tours)
        {
            var priorityMatch = tours.Where(tour => tour.Provider.Id == _options.PriorityProviderId)
                .OrderBy(tour => tour.PricePerGuest)
                .FirstOrDefault();

            foreach (var tour in tours)
                if (priorityMatch == null || priorityMatch.PricePerGuest >
                    tour.PricePerGuest + tour.PricePerGuest * _options.MaxPercentPrice)
                    priorityMatch = tour;

            return priorityMatch;
        }
    }
}