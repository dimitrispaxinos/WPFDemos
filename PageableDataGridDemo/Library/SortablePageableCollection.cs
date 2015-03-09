using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace PageableDataGrid.Library
{
    public class SortablePageableCollection<T> : PageableCollection<T>, ISortable
    {
        public SortablePageableCollection(IEnumerable<T> allObjects, int? entriesPerPage = null)
            : base(allObjects, entriesPerPage)
        {
        }

        public void Sort(string propertyName, string direction)
        {
            PropertyInfo prop = typeof(T).GetProperty(propertyName);

            if (string.IsNullOrEmpty(direction) || direction.ToLower() == "descending")
                AllObjects = new ObservableCollection<T>(AllObjects.OrderByDescending(x => prop.GetValue(x, null)));
            else
                AllObjects = new ObservableCollection<T>(AllObjects.OrderBy(x => prop.GetValue(x, null)));

            CurrentPageNumber = 1;
            SetCurrentPageItems();
        }
    }
}
