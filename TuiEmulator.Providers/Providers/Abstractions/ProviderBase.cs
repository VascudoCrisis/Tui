using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TuiEmulator.Common;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Models.Requests;
using TuiEmulator.Common.Options;
using TuiEmulator.Common.Specifications;
using TuiEmulator.Common.Specifications.Abstractions;
using TuiEmulator.Providers.Extensions;
using TuiEmulator.Providers.Repositories.Abstractions;
using TuiEmulator.Providers.Repositories.Static;

namespace TuiEmulator.Providers.Providers.Abstractions
{
    internal abstract class ProviderBase : IToursProviderService
    {
        private readonly ILocationsRepository _locationsRepository;

        private readonly Tour[] _tours;

        public ProviderBase(ILocationsRepository locationsRepository, ProviderOptions options)
        {
            _locationsRepository = locationsRepository;


            var provider = new TourProvider {Id = options.Id, Title = options.Title};

            _tours = Enumerable.Range(0, options.ToursCount)
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Select(i => StaticRepository.GetTour(provider))
                .ToArray();
        }

        public async Task<IEnumerable<City>> GetAllCitiesOfDeparture(CancellationToken token)
        {
            return await Task.FromResult(_tours.Select(tour => tour.CityOfDeparture).Uniq(city => city.Id));
        }

        public async Task<IEnumerable<City>> GetAllCitiesOdArrival(CancellationToken token)
        {
            return await Task.FromResult(_tours.Select(tour => tour.CityOfArrival).Uniq(city => city.Id));
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _locationsRepository.GetAllCities();
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _locationsRepository.GetAllCountries();
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await _locationsRepository.GetAllHotels();
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            return await _locationsRepository.GetHotelById(id);
        }

        public async Task<IEnumerable<Tour>> Search(SearchRequest request, CancellationToken token)
        {
            await Task.Delay(StaticRepository.Random.Next(3000, 17001), token);

            if (request.Fields != null)
            {
                var aggregatedSpecification = GetAggregatedSpecification(request.Fields);

                if (aggregatedSpecification == null)
                    return _tours.AsQueryable().Take(request.Take);

                return _tours.AsQueryable().Where(aggregatedSpecification).Take(request.Take);
            }

            return _tours.AsQueryable().Take(request.Take);
        }

        #region Utilities

        private Specification<Tour> GetAggregatedSpecification(SearchRequest.SearchFields fields)
        {
            Specification<Tour> spec = null;

            if (fields.CityOfDepartureId.HasValue)
                spec &= new CityOfDepartureSpecification(fields.CityOfDepartureId.Value);
            if (fields.CityOfTourId.HasValue)
                spec &= new TourCitySpecification(fields.CityOfTourId.Value);
            if (fields.NightsTo.HasValue)
                spec &= new NightsToCountSpecification(fields.NightsTo.Value);
            if (fields.NightsFrom.HasValue)
                spec &= new NightsFromCountSpecification(fields.NightsFrom.Value);
            if (fields.TourStartDate.HasValue)
                spec &= new TourStartDatepecification(fields.TourStartDate.Value);
            if (fields.GuestsCount.HasValue)
                spec &= new GuestsCountSpecification(fields.GuestsCount.Value);

            return spec;
        }

        #endregion
    }
}