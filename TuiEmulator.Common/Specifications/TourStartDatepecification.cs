using System;
using System.Linq.Expressions;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Specifications.Abstractions;

namespace TuiEmulator.Common.Specifications
{
    public class TourStartDatepecification : Specification<Tour>
    {
        private readonly DateTimeOffset _dateFrom;

        public TourStartDatepecification(DateTimeOffset dateFrom)
        {
            _dateFrom = dateFrom;
        }

        internal override Expression<Func<Tour, bool>> ToExpression()
        {
            return tour => tour.DateOfDeparture >= _dateFrom;
        }
    }
}