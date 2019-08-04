using System;
using System.Linq.Expressions;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Specifications.Abstractions;

namespace TuiEmulator.Common.Specifications
{
    /// <summary>
    ///     Фильтр по городу тура
    /// </summary>
    public class TourCitySpecification : Specification<Tour>
    {
        private readonly int _cityId;

        public TourCitySpecification(int cityId)
        {
            _cityId = cityId;
        }

        internal override Expression<Func<Tour, bool>> ToExpression()
        {
            return tour => tour.CityOfArrival.Id == _cityId;
        }
    }
}