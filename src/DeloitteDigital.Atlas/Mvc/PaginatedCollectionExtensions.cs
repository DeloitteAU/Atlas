using System.Collections.Generic;
using System.Linq;

namespace DeloitteDigital.Atlas.Mvc
{
    /// <summary>
    /// Contains extension methods to IEnumerable<T> type
    /// </summary>
    public static class PaginatedCollectionExtensions
    {
        /// <summary>
        /// Converts an IEnumerable&lt;T&gt; collection to a PaginatedList&lt;T&gt; collection
        /// </summary>
        /// <typeparam name="T">Collection item type</typeparam>
        /// <param name="collection">Original collection</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A paginated list collection</returns>
        public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> collection, int page, int pageSize)
        {
            var itemCount = collection.Count();
            var pagedCollection = collection.ApplyPagination(page, pageSize).ToArray();
            return new PaginatedList<T>(pagedCollection, page, pageSize, itemCount);
        }

        /// <summary>
        /// Applies pagination logic over a generic collection
        /// </summary>
        /// <param name="collection">A collection of item to be paginated</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size or number of items per page</param>
        /// <returns></returns>
        public static IEnumerable<T> ApplyPagination<T>(this IEnumerable<T> collection, int page, int pageSize)
        {
            return collection.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Applies pagination logic predicate on an existing query
        /// </summary>
        /// <typeparam name="T">Query item type</typeparam>
        /// <param name="query">Query</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns></returns>
        public static IQueryable<T> ApplyPaginationPredicate<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
