using Ecclipse.UWP.Model;
using Ecclipse.UWP.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using PaginatedGridView.Sample.UWP.Data;
using PaginatedGridView.Sample.UWP.Model;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaginatedGridView.Sample.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ImageDataTemplate = (DataTemplate)this.Resources["PictureDataTemplate"];
            ItemsCollectionUI = new PaginatedGridView<ImageItem>()
            {
                ItemsPerPage = 10,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 40, 0, 0),
                ItemTemplate = ImageDataTemplate
            };

            this.Loaded += MainPage_Loaded;
        }

        #region Props
        private int ItemsPerPage { get; set; } = 10;
        private PaginatedCollection<ImageItem> ItemsSource { get; set; }
        private PaginatedGridView<ImageItem> ItemsCollectionUI { get; set; }
        private DataTemplate ImageDataTemplate { get; set; }
        #endregion

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RootLayout.Children.Add(ItemsCollectionUI);

                ItemsSource = new PaginatedCollection<ImageItem>(Images.ItemsSource, ItemsPerPage);
                ItemsCollectionUI.PagedItemsSource = ItemsSource;

                PipsPagerUI.NumberOfPages = ItemsSource.PageCount;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message, ex, this.GetType().Name);
            }
        }

        private void PipsPagerUI_SelectedIndexChanged(PipsPager sender, PipsPagerSelectedIndexChangedEventArgs args)
        {
            try
            {
                ItemsCollectionUI.SetPage(sender.SelectedPageIndex);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message, ex, this.GetType().Name);
            }
        }
    }
}
