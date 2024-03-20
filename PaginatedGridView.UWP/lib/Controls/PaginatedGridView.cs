using Ecclipse.UWP.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ecclipse.UWP.UI.Xaml.Controls
{
    public partial class PaginatedGridView<T> : GridView
    {
        public PaginatedGridView() 
        {
            this.Loaded += PaginatedGridView_Loaded;
        }

        private int _pageIndex = 0;
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
                LoadPageItems();
            }
        }
        public int ItemsPerPage { get; set; }

        private PaginatedCollection<T> _itemsSource = null;
        public PaginatedCollection<T> PagedItemsSource
        {
            get
            {
                return _itemsSource;
            }
            set
            {
                _itemsSource = value;
            }
        }

        private void LoadPageItems()
        {
            if(PagedItemsSource != null)
            {
                var pageCollection = PagedItemsSource.GetPage(PageIndex);

                if(pageCollection != null && pageCollection.Count != 0)
                {
                    UpdateCollectionUI(pageCollection);
                }
                else this.ItemsSource = null;
            }
        }

        private void UpdateCollectionUI(IEnumerable<T> items)
        {
            if(items.Count() != 0)
            {
                this.ItemsSource = null;
                this.ItemsSource = items;
            }
            else this.ItemsSource = null;
        }

        private void PaginatedGridView_Loaded(object sender, RoutedEventArgs e) => LoadPageItems();

        public void SetPage(int pageIndex)
        {
            try
            {
                if(PagedItemsSource != null && PagedItemsSource.Count > 0)
                   UpdateCollectionUI(PagedItemsSource.GetPage(pageIndex));
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message, ex, this.GetType().Name);
            }
        }
    }
}
