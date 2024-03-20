using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ecclipse.UWP.Model
{
    /// <summary>
    /// This collection represents paginated collection for paged lists and collections creation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedCollection<T> : ObservableCollection<T>, IPaginatedCollection<T>
    {
        private int _itemsPerPage = 10;

        /// <summary>
        /// This property represents count of items per one page.
        /// </summary>
        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set
            {
                if(value <= 0)
                   throw new ArgumentException("Items per page should be greater than zero");
                _itemsPerPage = value;
            }
        }

        /// <summary>
        /// Pages counter.
        /// </summary>
        public int PageCount => (int)Math.Ceiling((double)Count / _itemsPerPage);

        public int CurrentPageIndex { get; set; }


        public PaginatedCollection(IEnumerable<T> items, int itemsPerPage) 
        {
            if(itemsPerPage <= 0)
               throw new ArgumentException("Items per page should be greater than zero");
            this._itemsPerPage = itemsPerPage;

            if(items != null)
               foreach(var item in items)
               this.Add(item);
        }

        /// <summary>
        /// Gets items from collection page part by index.
        /// </summary>
        /// <param name="pageIndex">Index of required page.</param>
        /// <returns>Page items collection</returns>
        /// <exception cref="ArgumentOutOfRangeException">Happens when page index is smaller than zero.</exception>
        public ObservableCollection<T> GetPage(int pageIndex)
        {
            if(pageIndex < 0 || pageIndex >= PageCount)
               throw new ArgumentOutOfRangeException(nameof(pageIndex));

            int startIndex = pageIndex * _itemsPerPage;

            return new ObservableCollection<T>
            (
                this.Skip(startIndex).Take(_itemsPerPage)
            );
        }

        public ObservableCollection<T> Create(int itemsPerPage, IEnumerable<T> items)
        {
            return new ObservableCollection<T>(items);
        }
    }
}
