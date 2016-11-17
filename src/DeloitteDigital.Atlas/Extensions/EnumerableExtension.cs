using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DeloitteDigital.Atlas.Refactoring;

namespace DeloitteDigital.Atlas.Extensions
{
    [LegacyCode]
    public static class EnumerableExtension
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            for (var i = 0; i < enumerable.Count(); i++)
            {
                var item = enumerable.ElementAt(i);
                action(item);
            }
        }

        public static IEnumerable<TResult> ForEach<T, TResult>( this IEnumerable<T> enumerable, Func<T, TResult> action )
        {
            for (var i = 0; i < enumerable.Count(); i++)
            {
                var item = enumerable.ElementAt(i);
                yield return action(item);
            }
        }
   
        /// <summary>
        /// It lets you iterate over all the elements in a generic fashion, then returns the Enumerable, thereby allowing chain-calling.
        /// </summary>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="iterator"></param>
        public static void Each<TEnumerable>(this IEnumerable<TEnumerable> enumerable, Action<TEnumerable> iterator)
        {
            for (var i = 0; i < enumerable.Count(); i++)
            {
                iterator(enumerable.ToArray()[i]);
            }
        }


        /// <summary>
        /// It lets you iterate over all the elements in a generic fashion, then returns the Enumerable, thereby allowing chain-calling.
        /// </summary>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="iterator"></param>
        public static void Each<TEnumerable>(this IEnumerable<TEnumerable> enumerable, Action<TEnumerable, int> iterator)
        {
            for (var index = 0; index < enumerable.Count(); index++)
            {
                iterator(enumerable.ToArray()[index], index);
            }
        }
    }
    
}
