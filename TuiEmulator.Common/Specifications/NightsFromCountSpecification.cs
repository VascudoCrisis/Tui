using System;
using System.Linq.Expressions;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Specifications.Abstractions;

namespace TuiEmulator.Common.Specifications
{
    /// <summary>
    ///     Фильтр по минимальному количеству ночей
    /// </summary>
    public class NightsFromCountSpecification : Specification<Tour>
    {
        private readonly int _value;

        public NightsFromCountSpecification(int value)
        {
            _value = value;
        }

        internal override Expression<Func<Tour, bool>> ToExpression()
        {
            return tour => tour.Nights >= _value;
        }
    }
}