using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace ProgressChartSample
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            CurrentStatus = StatusesEnum.Completed;
        }

        public IEnumerable<StatusesEnum> AvailableStatuses
        {
            get
            {
                return Enum.GetValues(typeof(StatusesEnum)).Cast<StatusesEnum>().ToList();
            }
        }

        private StatusesEnum _currentStatus;
        public StatusesEnum CurrentStatus
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                    _currentStatus = value;
                    SendPropertyChanged(() => CurrentStatus);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }
    }
}
