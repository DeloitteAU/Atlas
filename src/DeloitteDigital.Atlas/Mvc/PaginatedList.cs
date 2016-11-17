using System.Collections.Generic;

namespace DeloitteDigital.Atlas.Mvc
{
    /// <summary>
    /// Represents a paginated list
    /// </summary>
    public interface IPaginatedList
    {
        /// <summary>
        /// Page number
        /// </summary>
        int Page { get; }

        /// <summary>
        /// Previous page number
        /// </summary>
        int PreviousPage { get; }

        /// <summary>
        /// Next page number
        /// </summary>
        int NextPage { get; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// The number of pages
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets item count
        /// </summary>
        int ItemCount { get; }

        /// <summary>
        /// Gets item page begin index (base 1)
        /// </summary>
        int BeginIndex { get; }

        /// <summary>
        /// Gets item page end index
        /// </summary>
        int EndIndex { get; }
    }

    /// <summary>
    /// Represents a paginated list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T> : IPaginatedList
    {
        /// <summary>
        /// Inner item collection
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Initialises a new instance of the PaginatedList class
        /// </summary>
        /// <param name="items">Item collection</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size (number of items per page)</param>
        /// <param name="pageCount">Page count (total number of pages)</param>
        public PaginatedList(IEnumerable<T> items, int page, int pageSize, int itemCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            ItemCount = itemCount;
            PageCount = (itemCount % pageSize == 0) ? itemCount / pageSize : itemCount / pageSize + 1;
            PreviousPage = page > 1 ? page - 1 : 1;
            NextPage = page < PageCount ? page + 1 : PageCount;
            BeginIndex = PageSize * Page - PageSize + 1;
            EndIndex = (BeginIndex + PageSize - 1) > ItemCount ? ItemCount : (BeginIndex + PageSize - 1);
        }

        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Previous page number
        /// </summary>
        public int PreviousPage { get; private set; }

        /// <summary>
        /// Next page number
        /// </summary>
        public int NextPage { get; private set; }

        /// <summary>
        /// Page size (number of items per page)
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Page count (total number of pages)
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// Item count
        /// </summary>
        public int ItemCount { get; private set; }

        /// <summary>
        /// Item page begin index (base 1)
        /// </summary>
        public int BeginIndex { get; private set; }

        /// <summary>
        /// Item page end index
        /// </summary>
        public int EndIndex { get; private set; }
    }
}
