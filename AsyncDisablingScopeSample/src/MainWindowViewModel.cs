using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AsyncDisablingScopeSample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IDisableableCommand _addCommand;
        public IDisableableCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new DisableableCommandAsync(
                        AddCommandHandler,
                        obj => true,
                        new List<IDisableableCommand>() { RemoveCommand, UpdateCommand });
                return _addCommand;
            }
        }

        private IDisableableCommand _removeCommand;
        public IDisableableCommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                    _removeCommand = new DisableableCommandAsync(RemoveCommandHandler, (obj) => true);
                return _removeCommand;
            }
        }

        private IDisableableCommand _updateCommand;
        public IDisableableCommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                    _updateCommand = new DisableableCommandAsync(UpdateCommandHandler, obj => true);
                return _updateCommand;
            }

        }

        private Task UpdateCommandHandler(object arg)
        {
            throw new NotImplementedException();
        }

        private Task RemoveCommandHandler(object arg)
        {
            throw new NotImplementedException();
        }

        private async Task AddCommandHandler(object arg)
        {
            await Task.Delay(5000);
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        #endregion
    }
}
