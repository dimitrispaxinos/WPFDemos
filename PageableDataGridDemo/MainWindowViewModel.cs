using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

using PageableDataGrid.Library;

namespace PageableDataGrid
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private SortablePageableCollection<Contact> _contacts;
        public SortablePageableCollection<Contact> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                if (_contacts != value)
                {
                    _contacts = value;
                    SendPropertyChanged(() => Contacts);
                }
            }
        }

        private Contact _newContact;
        public Contact NewContact
        {
            get { return _newContact; }
            set
            {
                if (_newContact != value)
                {
                    _newContact = value;
                    SendPropertyChanged(() => NewContact);
                }
            }
        }

        public ICommand GoToNextPageCommand { get; private set; }
        public ICommand GoToPreviousPageCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand AddNewContactCommand { get; private set; }

        public List<int> EntriesPerPageList
        {
            get
            {
                return new List<int>() { 5, 10, 15 };
            }
        }

        public MainWindowViewModel()
        {
            // Create Contancts
            Contacts = new SortablePageableCollection<Contact>(CreateInitialContacts());

            // Create Commands 
            GoToNextPageCommand = new RelayCommand(a => Contacts.GoToNextPage());
            GoToPreviousPageCommand = new RelayCommand(a => Contacts.GoToPreviousPage());
            RemoveItemCommand = new RelayCommand(item => Contacts.Remove(item as Contact));
            AddNewContactCommand = new RelayCommand(item => AddNewContact());

            NewContact = new Contact();
        }

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

        private IEnumerable<Contact> CreateInitialContacts()
        {
            var list = new List<Contact>();

            for (int i = 1; i < 90; i++)
            {
                var newContact = new Contact() { Name = "ContactName " + i.ToString("000"), Email = "address" + i.ToString("000") + "@test.com" };
                list.Add(newContact);
            }
            return list;
        }

        private void AddNewContact()
        {
            if (NewContact != null && !string.IsNullOrEmpty(NewContact.Name) && !string.IsNullOrEmpty(NewContact.Email))
            {
                Contacts.Add(NewContact);
                NewContact = new Contact();
            }
        }
    }
}
