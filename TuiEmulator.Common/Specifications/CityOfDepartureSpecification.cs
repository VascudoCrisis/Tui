using System;
using System.Linq.Expressions;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Specifications.Abstractions;

namespace TuiEmulator.Common.Specifications
{
    public class CityOfDepartureSpecification : Specification<Tour>
    {
        private readonly int _cityId;

        public CityOfDepartureSpecification(int cityId)
        {
            _cityId = cityId;
        }

        internal override Expression<Func<Tour, bool>> ToExpression()
        {
            return tour => tour.CityOfDeparture.Id == _cityId;
        }
    }
}