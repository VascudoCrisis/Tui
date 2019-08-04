using System;
using System.Linq.Expressions;

namespace TuiEmulator.Common.Specifications.Abstractions
{
    /// <summary>
    ///     Объединяющая спецификация
    /// </summary>
    /// <typeparam name="TType">Тип элемента</typeparam>
    internal sealed class AndSpecification<TType> : Specification<TType>
    {
        private readonly Specification<TType> _left;
        private readonly Specification<TType> _right;

        internal AndSpecification(Specification<TType> left, Specification<TType> right)
        {
            _left = left;
            _right = right;
        }

        internal override Expression<Func<TType, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var andExpression = Expression.AndAlso(
                leftExpression.Body, Expression.Invoke(rightExpression, leftExpression.Parameters));

            return Expression.Lambda<Func<TType, bool>>(andExpression, leftExpression.Parameters);
        }
    }
}