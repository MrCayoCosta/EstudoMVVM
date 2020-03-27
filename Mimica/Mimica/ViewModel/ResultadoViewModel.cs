using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mimica.ViewModel
{
    public class ResultadoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }
    }

}
