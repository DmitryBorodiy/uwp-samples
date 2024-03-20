using System.Collections.ObjectModel;

namespace Ecclipse.UWP.Model
{
    public interface IPaginatedCollection<T>
    {
        int ItemsPerPage { get; set; }
        int PageCount { get; }
        int CurrentPageIndex { get; set; }

        ObservableCollection<T> GetPage(int pageIndex);
    }
}
