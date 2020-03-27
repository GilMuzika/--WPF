using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exercise_2
{
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        //INotifyPropertyChanged members and methods
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //IDataErrorInfo members and methods
        public string Error => string.Empty;
        public string this[string propertyName]
        {
            get => GetError();
        }
        protected virtual string GetError() { return string.Empty; }
    }
}
