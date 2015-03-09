using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace PageableDataGrid.Library
{
    public class PageableCollection<T> : INotifyPropertyChanged
    {
        #region Properties

        // Default Entries per page Number
        private int _pageSize = 5;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    SendPropertyChanged(() => PageSize);
                    Reset();
                }
            }
        }

        public int TotalPagesNumber
        {
            get
            {
                if (AllObjects != null && PageSize > 0)
                {
                    return (AllObjects.Count - 1) / PageSize + 1;
                }
                return 0;
            }
        }

        private int _currentPageNumber = 1;
        public int CurrentPageNumber
        {
            get
            {
                return _currentPageNumber;
            }

            protected set
            {
                if (_currentPageNumber != value)
                {
                    _currentPageNumber = value;
                    SendPropertyChanged(() => CurrentPageNumber);
                }
            }
        }

        private ObservableCollection<T> _currentPageItems;
        public ObservableCollection<T> CurrentPageItems
        {
            get
            {
                return _currentPageItems;
            }
            private set
            {
                if (_currentPageItems != value)
                {
                    _currentPageItems = value;
                    SendPropertyChanged(() => CurrentPageItems);
                }
            }
        }

        protected ObservableCollection<T> AllObjects { get; set; }

        #endregion

        #region Constructors

        protected PageableCollection()
        {
        }

        public PageableCollection(IEnumerable<T> allObjects, int? entriesPerPage = null)
        {
            AllObjects = new ObservableCollection<T>(allObjects);

            if (entriesPerPage != null)
                PageSize = (int)entriesPerPage;

            SetCurrentPageItems();
        }

        #endregion

        #region Public Methods

        public void GoToNextPage()
        {
            if (CurrentPageNumber != TotalPagesNumber)
            {
                CurrentPageNumber++;
                SetCurrentPageItems();
            }
        }

        public void GoToPreviousPage()
        {
            if (CurrentPageNumber > 1)
            {
                CurrentPageNumber--;
                SetCurrentPageItems();
            }
        }

        public virtual void Remove(T item)
        {
            // Remove Item from the original collection
            AllObjects.Remove(item);

            // Update the total number of pages
            SendPropertyChanged(() => TotalPagesNumber);

            // if the last item on the last page is removed
            // then go to the previous page
            if (CurrentPageNumber > TotalPagesNumber)
                CurrentPageNumber--;

            SetCurrentPageItems();
        }

        public virtual void Add(T item)
        {
            // Go back to the first page
            CurrentPageNumber = 1;
            SetCurrentPageItems();

            // Keep the same size and put it on top
            CurrentPageItems.RemoveAt(CurrentPageItems.Count - 1);
            CurrentPageItems.Insert(0, item);

            // Add it to the original collection
            AllObjects.Add(item);
            SendPropertyChanged(() => TotalPagesNumber);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        public void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        #endregion

        #region Private Methods

        protected void SetCurrentPageItems()
        {
            int upperLimit = CurrentPageNumber * PageSize;

            CurrentPageItems =
                new ObservableCollection<T>(
                    AllObjects.Where(x => AllObjects.IndexOf(x) > upperLimit - (PageSize + 1) && AllObjects.IndexOf(x) < upperLimit));
        }

        private void Reset()
        {
            CurrentPageNumber = 1;
            SetCurrentPageItems();
            SendPropertyChanged(() => TotalPagesNumber);
        }

        #endregion
    }
}
