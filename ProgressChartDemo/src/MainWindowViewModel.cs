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
            CurrentStatus = BlogPostStatusEnum.Written;
        }

        public IEnumerable<BlogPostStatusEnum> AvailableStatuses
        {
            get
            {
                return Enum.GetValues(typeof(BlogPostStatusEnum)).Cast<BlogPostStatusEnum>().ToList();
            }
        }

        private BlogPostStatusEnum _currentStatus;
        public BlogPostStatusEnum CurrentStatus
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
