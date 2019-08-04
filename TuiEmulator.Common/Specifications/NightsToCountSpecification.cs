using System;
using System.Linq.Expressions;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Specifications.Abstractions;

namespace TuiEmulator.Common.Specifications
{
    /// <summary>
    ///     Фильтр по максимальному количеству ночей
    /// </summary>
    public class NightsToCountSpecification : Specification<Tour>
    {
        private readonly int _value;

        public NightsToCountSpecification(int value)
        {
            _value = value;
        }

        internal override Expression<Func<Tour, bool>> ToExpression()
        {
            return tour => tour.Nights <= _value;
        }
    }
}