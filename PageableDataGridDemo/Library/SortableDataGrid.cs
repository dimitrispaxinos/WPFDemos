using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PageableDataGrid.Library
{
    public class SortableDataGrid : DataGrid
    {
        #region Dependency Properties

        // The main collection of the Grid. It should be of type ISortable in order to be sortable.
        public ISortable FullItemsSource
        {
            get { return (ISortable)GetValue(FullItemsSourceProperty); }
            set { SetValue(FullItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty FullItemsSourceProperty = DependencyProperty.Register("FullItemsSource", typeof(ISortable), typeof(SortableDataGrid));
        #endregion

        #region Private Properties

        private ListSortDirection? _currentSortDirection;
        private DataGridColumn _currentSortColumn;

        #endregion

        #region Event Handlers

        // Get the current sort column from XAML or sort using the first column of the Grid.
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // The current sorted column must be specified in XAML.
            _currentSortColumn = Columns.FirstOrDefault(c => c.SortDirection.HasValue);

            // if not, then take the first column of the grid and set the sort direction to ascending
            if (_currentSortColumn == null)
            {
                _currentSortColumn = Columns.First();
                _currentSortColumn.SortDirection = ListSortDirection.Ascending;
            }

            _currentSortDirection = _currentSortColumn.SortDirection;
        }

        // Deactivate the default Grid sorting, call the ISortbleSorting
        protected override void OnSorting(DataGridSortingEventArgs eventArgs)
        {
            eventArgs.Handled = true;

            _currentSortColumn = eventArgs.Column;

            var direction = (_currentSortColumn.SortDirection != ListSortDirection.Ascending)
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            // Call ISortable Sorting to sort the complete collection
            if (FullItemsSource != null)
            {
                FullItemsSource.Sort(_currentSortColumn.SortMemberPath, direction.ToString());
            }

            _currentSortColumn.SortDirection = null;

            _currentSortColumn.SortDirection = direction;

            _currentSortDirection = direction;
        }

        // Restores the sorting direction every time the source gets updated e.g. the page is changed
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            if (_currentSortColumn != null)
                _currentSortColumn.SortDirection = _currentSortDirection;
        }

        #endregion
    }
}
