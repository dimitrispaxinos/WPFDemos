using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace WatermarkSample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private const string PasswordBoxWatermarkText1 = "PasswordBoxWatermark";
        private const string ComboBoxWatermarkText1 = "ComboBoxWatermark";
        private const string TextBoxWatermarkText1 = "TextBoxWatermark";

        private const string PasswordBoxWatermarkText2 = "PasswordBoxWatermark is changed";
        private const string ComboBoxWatermarkText2 = "ComboBoxWatermark is changed";
        private const string TextBoxWatermarkText2 = "TextBoxWatermark is changed";

        private bool _areInitialWaterMarks;

        public MainWindowViewModel()
        {
            ChangeWatermarkTexts();
        }

        private bool _isPasswordBoxVisible = true;
        public bool IsPasswordBoxVisible
        {
            get { return _isPasswordBoxVisible; }
            set
            {
                if (_isPasswordBoxVisible != value)
                {
                    _isPasswordBoxVisible = value;
                    SendPropertyChanged(() => IsPasswordBoxVisible);
                }
            }
        }

        private bool _isComboBoxVisible = true;
        public bool IsComboBoxVisible
        {
            get { return _isComboBoxVisible; }
            set
            {
                if (_isComboBoxVisible != value)
                {
                    _isComboBoxVisible = value;
                    SendPropertyChanged(() => IsComboBoxVisible);
                }
            }
        }

        private bool _isTextBoxVisible = true;
        public bool IsTextBoxVisible
        {
            get { return _isTextBoxVisible; }
            set
            {
                if (_isTextBoxVisible != value)
                {
                    _isTextBoxVisible = value;
                    SendPropertyChanged(() => IsTextBoxVisible);
                }
            }
        }

        private string _passwordBoxWatermark;
        public string PasswordBoxWatermark
        {
            get { return _passwordBoxWatermark; }
            set
            {
                if (_passwordBoxWatermark != value)
                {
                    _passwordBoxWatermark = value;
                    SendPropertyChanged(() => PasswordBoxWatermark);
                }
            }
        }

        private string _comboBoxWatermark;
        public string ComboBoxWatermark
        {
            get { return _comboBoxWatermark; }
            set
            {
                if (_comboBoxWatermark != value)
                {
                    _comboBoxWatermark = value;
                    SendPropertyChanged(() => ComboBoxWatermark);
                }
            }
        }

        private string _textBoxWatermark;
        public string TextBoxWatermark
        {
            get { return _textBoxWatermark; }
            set
            {
                if (_textBoxWatermark != value)
                {
                    _textBoxWatermark = value;
                    SendPropertyChanged(() => TextBoxWatermark);
                }
            }
        }

        public void ChangeWatermarkTexts()
        {
            PasswordBoxWatermark = _areInitialWaterMarks ? PasswordBoxWatermarkText2 : PasswordBoxWatermarkText1;
            ComboBoxWatermark = _areInitialWaterMarks ? ComboBoxWatermarkText2 : ComboBoxWatermarkText1;
            TextBoxWatermark = _areInitialWaterMarks ? TextBoxWatermarkText2 : TextBoxWatermarkText1;

            _areInitialWaterMarks = !_areInitialWaterMarks;
            //SendPropertyChanged("");
        }

        #region INotifyPropertyChangedImplementation

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        public virtual void SendPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            SendPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
