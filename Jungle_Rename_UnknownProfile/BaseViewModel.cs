using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_Rename_UnknownProfile
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void Set<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (!field.Equals(value))
            {
                field = value;
                OnPropertyChanged();
            }
        }
    }
}
