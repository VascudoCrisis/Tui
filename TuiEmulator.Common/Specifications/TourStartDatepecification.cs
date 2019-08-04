using System;
using System.Linq.Expressions;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Specifications.Abstractions;

namespace TuiEmulator.Common.Specifications
{
    /// <summary>
    ///     Фильтр по дате начала тура
    /// </summary>
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