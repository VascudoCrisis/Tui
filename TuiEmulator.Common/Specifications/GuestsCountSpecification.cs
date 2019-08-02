using System;
using System.Linq.Expressions;
using TuiEmulator.Common.Models;
using TuiEmulator.Common.Specifications.Abstractions;

namespace TuiEmulator.Common.Specifications
{
    public class GuestsCountSpecification : Specification<Tour>
    {
        private readonly int _count;

        public GuestsCountSpecification(int count)
        {
            _count = count;
        }

        internal override Expression<Func<Tour, bool>> ToExpression()
        {
            return tour => tour.MaxGuestCount >= _count;
        }
    }
}