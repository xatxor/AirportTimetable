using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirportTimetableWPF.Models
{
    public class Property : INotifyPropertyChanged
    {
        public int Obj { get; set; }
        public Property()
        {
            Obj = 12;
        }
        public Property(int obj)
        {
            Obj = obj;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
