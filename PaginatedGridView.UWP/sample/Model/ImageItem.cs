using System;
using Windows.UI.Xaml.Media.Imaging;

namespace PaginatedGridView.Sample.UWP.Model
{
    public class ImageItem
    {
        public string Title { get; set; } = String.Empty;
        public BitmapImage ImageSource { get; set; }

        public ImageItem(string title = null, BitmapImage imageSource = null)
        {
            Title = title;
            ImageSource = imageSource;
        }
    }
}
