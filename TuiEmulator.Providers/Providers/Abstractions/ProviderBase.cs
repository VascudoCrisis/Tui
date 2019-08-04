using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TuiEmulator.Common.Enums;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Options;
using TuiEmulator.Common.Requests;
using TuiEmulator.Common.Services;
using TuiEmulator.Common.Specifications;
using TuiEmulator.Common.Specifications.Abstractions;
using TuiEmulator.Providers.Extensions;
using TuiEmulator.Providers.Repositories.Abstractions;
using TuiEmulator.Providers.Repositories.Static;

namespace TuiEmulator.Providers.Providers.Abstractions
{
    /// <summary>
    ///     Базовый класс поставщика туров
    /// </summary>
    internal abstract class ProviderBase : IToursProviderService
    {
        private readonly ILocationsRepository _locationsRepository;

        private readonly Tour[] _tours;

        public ProviderBase(ILocationsRepository locationsRepository, ProviderOptions options)
        {
            _locationsRepository = locationsRepository;

            var provider = new TourProvider {Id = options.Id, Title = options.Title};

            _tours = Enumerable.Range(0, (int) options.ToursCount)
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Select(i => StaticRepository.GetTour(provider, i))
                .ToArray();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<City>> GetAllCitiesOfDeparture(CancellationToken token)
        {
            return await Task.FromResult(_tours.Select(tour => tour.CityOfDeparture).Uniq(city => city.Id));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<City>> GetAllCitiesOdArrival(CancellationToken token)
        {
            return await Task.FromResult(_tours.Select(tour => tour.CityOfArrival).Uniq(city => city.Id));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _locationsRepository.GetAllCities();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _locationsRepository.GetAllCountries();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await _locationsRepository.GetAllHotels();
        }

        /// <inheritdoc />
        public async Task<Hotel> GetHotelById(int id)
        {
            return await _locationsRepository.GetHotelById(id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Tour>> Search(SearchRequest request, CancellationToken token)
        {
            await Task.Delay(StaticRepository.Random.Next(3000, 17001), token);

            if (request.Fields != null)
            {
                var aggregatedSpecification = GetAggregatedSpecification(request.Fields);

                if (aggregatedSpecification == null)
                    return _tours.AsQueryable().Take(request.Take);

                return Sort(_tours.AsQueryable().Where(aggregatedSpecification), request.OrderBy,
                    request.OrderDirection).Take(request.Take);
            }

            return _tours.AsQueryable().Take(request.Take);
        }


        #region Utilities

        /// <summary>
        ///     Отсортировать коллекцию туров
        /// </summary>
        /// <param name="tours">Коллекция туров</param>
        /// <param name="orderBy">Поле для сортировки</param>
        /// <param name="orderDirection">Направление сортировки</param>
        /// <returns>Отсортированная коллекция туров</returns>
        /// <exception cref="ArgumentException">Поле сортировки не найдено</exception>
        private IQueryable<Tour> Sort(IQueryable<Tour> tours, OrderBy orderBy, OrderDirection orderDirection)
        {
            switch (orderBy)
            {
                case OrderBy.Date:
                    return SortByDirection(tours, tour => tour.DateOfDeparture, orderDirection);
                case OrderBy.Name:
                    return SortByDirection(tours, tour => tour.Title, orderDirection);
                case OrderBy.Price:
                    return SortByDirection(tours, tour => tour.PricePerGuest, orderDirection);
            }

            throw new ArgumentException(nameof(orderBy));
        }

        /// <summary>
        ///     Применение направления сортировки
        /// </summary>
        /// <param name="tours">Коллекция туров</param>
        /// <param name="field">Поле</param>
        /// <param name="direction">Направление</param>
        /// <typeparam name="TParameter">Тип поля</typeparam>
        /// <returns>Отсортированная коллекция туров</returns>
        private IQueryable<Tour> SortByDirection<TParameter>(IQueryable<Tour> tours,
            Expression<Func<Tour, TParameter>> field,
            OrderDirection direction)
        {
            if (direction == OrderDirection.Asc)
                return tours.OrderBy(field);
            return tours.OrderByDescending(field);
        }

        /// <summary>
        ///     Получение обобщенной спецификации
        /// </summary>
        /// <param name="fields">Поля поиска</param>
        /// <returns>Обобщенная спецификация</returns>
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