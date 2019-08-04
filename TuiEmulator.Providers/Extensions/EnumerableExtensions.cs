using System;
using System.Collections.Generic;

namespace TuiEmulator.Providers.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Получение уникальных значений из коллеции.
        ///     При наличии нескольких значений, выбирает первое и отсеивает оставшиеся
        /// </summary>
        /// <param name="collection">Коллеция элементов</param>
        /// <param name="getProperty">Функция получения значения свойства</param>
        /// <typeparam name="TType">Тип элемента</typeparam>
        /// <typeparam name="TProperty">Тип свойства</typeparam>
        /// <returns>Отфильтрованный список элементов</returns>
        public static IEnumerable<TType> Uniq<TType, TProperty>(this IEnumerable<TType> collection,
            Func<TType, TProperty> getProperty)
        {
            var hs = new HashSet<TProperty>();

            foreach (var item in collection)
                if (hs.Add(getProperty(item)))
                    yield return item;
        }
    }
}