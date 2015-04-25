using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncDisablingScopeSample
{
    public class DisableableCommandAsync : IDisableableCommand
    {
        #region Fields

        private readonly Predicate<object> _canExecute;
        private readonly Func<object, Task> _asyncExecute;
        private readonly List<IDisableableCommand> _disablables;
        private bool _externalCanExecute = true;

        #endregion

        #region Constructors

        public DisableableCommandAsync(Func<object, Task> asyncExecute)
            : this(asyncExecute, null)
        {
        }

        public DisableableCommandAsync(Func<object, Task> asyncExecute, Predicate<object> canExecute)
        {
            if (asyncExecute == null)
            {
                throw new ArgumentNullException("_asyncExecute");
            }

            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// DisableableCommandAsync Constructor
        /// </summary>
        /// <param name="asyncExecute">Executing Delegate</param>
        /// <param name="canExecute">Predicate for enabling/disabling</param>
        /// <param name="disablables">List of IDisaeable Items to be disabled while executing</param>
        /// <param name="disableWhileInProgress">Disable the Command itself while executing</param>
        public DisableableCommandAsync(Func<object,Task> asyncExecute,
               Predicate<object> canExecute,
               IEnumerable<IDisableableCommand> disablables = null,
               bool disableWhileInProgress = false)
                : this(asyncExecute, canExecute)
        {
            if (disablables != null)
            {
                _disablables = disablables.ToList();
                if (disableWhileInProgress)
                    _disablables.Add(this);
            }
        }

        #endregion

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (!_externalCanExecute)
                return false;

            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            if (_disablables != null)
            {
                using (var scope = new DisablingScope(_disablables))
                {
                    await ExecuteAsync(parameter);
                }
                CommandManager.InvalidateRequerySuggested();
            }
            else
                await ExecuteAsync(parameter);
        }

        protected virtual async Task ExecuteAsync(object parameter)
        {
            await _asyncExecute(parameter);
        }

        #endregion

        public void Enable()
        {
            _externalCanExecute = true;
        }

        public void Disable()
        {
            _externalCanExecute = false;
        }
    }
}
