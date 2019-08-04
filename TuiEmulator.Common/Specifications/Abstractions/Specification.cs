using System;
using System.Linq.Expressions;

namespace TuiEmulator.Common.Specifications.Abstractions
{
    public abstract class Specification<T>
    {
        internal abstract Expression<Func<T, bool>> ToExpression();


        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            if (left == null) return right;
            return new AndSpecification<T>(left, right);
        }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.ToExpression();
        }
    }
}